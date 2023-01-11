using AMP.Domain.Entities.Messaging;
using AMP.Processors.Commands.Messaging;
using AMP.Processors.Dtos.Messaging;
using AMP.Processors.PageDtos.Messaging;
using AMP.Processors.Workers.BackgroundWorker;
using Microsoft.AspNetCore.Http;

namespace AMP.Processors.Processors.Messaging;

[Processor]
public class ConversationProcessor : ProcessorBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBackgroundWorker _worker;

    private string UserId => _httpContextAccessor.HttpContext.User
        .FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public ConversationProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache,
        IHttpContextAccessor httpContextAccessor, IBackgroundWorker worker) 
        : base(uow, mapper, cache)
    {
        _uow = uow;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _worker = worker;
    }

    public async Task<Result<string>> Save(ConversationCommand command)
    {
        var convo = Conversation.Create(command.FirstParticipantId ?? UserId,
            command.SecondParticipantId);
        await _uow.Conversations.InsertAsync(convo);
        await _uow.SaveChangesAsync();
        return new Result<string>(convo.Id);
    }

    public async Task<Result<ConversationDto>> GetAsync(string id)
    {
        var convo = await _uow.Conversations.GetAsync(id);
        return new Result<ConversationDto>(_mapper.Map<ConversationDto>(convo));
    }
    
    public async Task<Result<PaginatedList<ConversationPageDto>>> GetPageAsync(PaginatedCommand command)
    {
        var convo = await _uow.Conversations.GetConversationPage(command, UserId,
            new CancellationToken());
        foreach (var conversation in convo.Data)
        {
            conversation.SetUnreadMessages(UserId);
        }
        return new Result<PaginatedList<ConversationPageDto>>(_mapper.Map<PaginatedList<ConversationPageDto>>(convo));
    }

    public async Task<Result<bool>> MarkAsRead(string id)
    {
        var convo = await _uow.Conversations.GetAsync(id);
        if (convo is null) return new Result<bool>
            (new InvalidIdException($"Conversation with id: {id} not found"));
        var receivedMsgs = convo.Messages.Where(x => x.ReceiverId == UserId);
        foreach (var convoMessage in receivedMsgs)
        {
            convoMessage.IsRead();
            convoMessage.IgnoreDateModified = true;
        }

        convo.IgnoreDateModified = true;
        await _uow.Conversations.UpdateAsync(convo);
        await _uow.SaveChangesAsync();
        _worker.ServeHub(DataCountType.Chats, UserId);
        return true;
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var convo = await _uow.Conversations.GetAsync(id);
        if (convo is null) return new Result<bool>(new NullReferenceException());
        await _uow.Conversations.SoftDeleteAsync(convo);
        await _uow.SaveChangesAsync();
        return new Result<bool>(true);
    }
}