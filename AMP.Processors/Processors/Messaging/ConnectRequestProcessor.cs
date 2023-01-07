using AMP.Domain.Entities.Messaging;
using AMP.Processors.Commands.Messaging;
using AMP.Processors.Dtos.Messaging;
using AMP.Processors.PageDtos.Messaging;
using Microsoft.AspNetCore.Http;

namespace AMP.Processors.Processors.Messaging;

[Processor]
public class ConnectRequestProcessor : ProcessorBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ConnectRequestProcessor(IUnitOfWork uow, IMapper mapper, 
        IMemoryCache cache, IHttpContextAccessor httpContextAccessor) 
        : base(uow, mapper, cache)
    {
        _uow = uow;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Result<string>> Save(ConnectRequestCommand command)
    {
        var request = ConnectRequest.Create(command.InviterId ??
            _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            , command.InviteeId);
        await _uow.ConnectRequests.InsertAsync(request);
        await _uow.SaveChangesAsync();
        return new Result<string>(request.Id);
    }

    public async Task<Result<ConnectRequestDto>> GetAsync(string id)
    {
        var request = await _uow.ConnectRequests.GetAsync(id);
        return new Result<ConnectRequestDto>(_mapper.Map<ConnectRequestDto>(request));
    }
    
    public async Task<Result<PaginatedList<ConnectRequestPageDto>>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _uow.ConnectRequests.GetChatInvitesPage(command, 
            _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            new CancellationToken());
        return new Result<PaginatedList<ConnectRequestPageDto>>(
            _mapper.Map<PaginatedList<ConnectRequestPageDto>>(page));
    }

    public async Task<Result<bool>> Accept(string id)
    {
        var request = await _uow.ConnectRequests.GetAsync(id);
        if (request.Id is null) return new Result<bool>(new NullReferenceException());
        var convo = Conversation.Create(request.InviterId, request.InviteeId);
        await _uow.Conversations.InsertAsync(convo);
        await _uow.SaveChangesAsync();
        return new Result<bool>(true);
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var request = await _uow.ConnectRequests.GetAsync(id);
        if (request.Id is null) return new Result<bool>(new NullReferenceException());
        await _uow.ConnectRequests.SoftDeleteAsync(request);
        await _uow.SaveChangesAsync();
        return new Result<bool>(true);
    }
}