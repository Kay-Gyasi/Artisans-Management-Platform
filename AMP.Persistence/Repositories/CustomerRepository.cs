using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    public class CustomerRepository : RepositoryBase<Customers>, ICustomerRepository
    {
        protected CustomerRepository(AmpDbContext context, ILogger<Customers> logger) : base(context, logger)
        {
        }
    }
}