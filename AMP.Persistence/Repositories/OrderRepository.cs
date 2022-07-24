using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Orders>, IOrderRepository
    {
        protected OrderRepository(AmpDbContext context, ILogger<Orders> logger) : base(context, logger)
        {
        }
    }
}