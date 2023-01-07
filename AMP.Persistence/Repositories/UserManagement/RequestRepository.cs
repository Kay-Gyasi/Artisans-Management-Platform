using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Repositories.BusinessManagement;

namespace AMP.Persistence.Repositories.UserManagement
{
    [Repository]
    public class RequestRepository : RepositoryBase<Request>, IRequestRepository
    {
        public RequestRepository(AmpDbContext context, ILogger<Request> logger) : base(context, logger)
        {
        }
    }
}