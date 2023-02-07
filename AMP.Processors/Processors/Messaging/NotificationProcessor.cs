using AMP.Processors.Dtos.Messaging;
using AMP.Processors.PageDtos.Messaging;

namespace AMP.Processors.Processors.Messaging;

[Processor]
public class NotificationProcessor : Processor
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public NotificationProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) 
        : base(uow, mapper, cache)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<Result<NotificationDto>> GetAsync(string id)
    {
        var notification = await _uow.Notifications.GetAsync(id);
        return new Result<NotificationDto>(_mapper.Map<NotificationDto>(notification));
    }
    
    public async Task<Result<PaginatedList<NotificationPageDto>>> GetPageAsync(PaginatedCommand command)
    {
        var page = await _uow.Notifications.GetPage(command, new CancellationToken());
        return new Result<PaginatedList<NotificationPageDto>>(
            _mapper.Map<PaginatedList<NotificationPageDto>>(page));
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var notification = await _uow.Notifications.GetAsync(id);
        if (notification is null) return new Result<bool>(new NullReferenceException());
        await _uow.Notifications.SoftDeleteAsync(notification);
        await _uow.SaveChangesAsync();
        return new Result<bool>(true);
    }

    public async Task<Result<bool>> MarkAsRead(string id)
    {
        var notification = await _uow.Notifications.GetAsync(id);
        if (notification is null) return new Result<bool>(new NullReferenceException());
        notification.IsSeen();
        await _uow.Notifications.UpdateAsync(notification);
        await _uow.SaveChangesAsync();
        return new Result<bool>(true);
    }
}