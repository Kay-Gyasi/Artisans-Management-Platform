using AMP.Domain.Entities.Messaging;
using AMP.Processors.Commands.Messaging;
using AMP.Processors.Dtos.Messaging;
using AMP.Processors.Workers.BackgroundWorker;
using Microsoft.AspNetCore.Http;

namespace AMP.Processors.Processors.Messaging;

[Processor]
public class ChatMessageProcessor : ProcessorBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IBackgroundWorker _worker;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string UserId => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public ChatMessageProcessor(IUnitOfWork uow, 
        IMapper mapper, IMemoryCache cache,
        IBackgroundWorker worker, IHttpContextAccessor httpContextAccessor) 
        : base(uow, mapper, cache)
    {
        _uow = uow;
        _mapper = mapper;
        _worker = worker;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> Save(ChatMessageCommand command)
    {
        command.SenderId ??= UserId;
        if (!await _uow.Conversations.IsConnected(command.SenderId, command.ReceiverId, command.ConversationId))
            return new Result<string>(new InvalidIdException());
        var message = ChatMessage
            .Create(command.ConversationId, command.Message)
            .SentBy(command.SenderId)
            .To(command.ReceiverId);
        await _uow.ChatMessages.InsertAsync(message);
        var convo = await _uow.Conversations.GetWithoutMessages(command.ConversationId);
        convo.IsModified();
        await _uow.SaveChangesAsync();
        _worker.ServeHub(DataCountType.Chats, command.ReceiverId);
        _worker.ServeHub(DataCountType.RefreshChat, command.ReceiverId, command.ConversationId);
        return new Result<string>(message.Id);
    }

    public async Task<int> GetUnreadMessages(string userId)
    {
        return await _uow.ChatMessages.GetUnreadMessageCount(userId);
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var message = await _uow.ChatMessages.GetAsync(id);
        if (message is null) return new Result<bool>(new NullReferenceException());
        await _uow.ChatMessages.SoftDeleteAsync(message);
        await _uow.SaveChangesAsync();
        return new Result<bool>(true);
    }
}