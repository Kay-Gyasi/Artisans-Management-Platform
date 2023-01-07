using AMP.Domain.Entities.Messaging;
using AMP.Processors.Repositories.Messaging;

namespace AMP.Persistence.Repositories.Messaging;

[Repository]
public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
{
    public NotificationRepository(AmpDbContext context, ILogger<Notification> logger) : base(context, logger)
    {
    }
}