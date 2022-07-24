using System.Threading.Tasks;
using AMP.Persistence.Database;
using AMP.Processors.Repositories;
using AMP.Processors.Repositories.Administration;
using AMP.Processors.Repositories.UoW;

namespace AMP.Persistence.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AmpDbContext _dbContext;
        
        public UnitOfWork(AmpDbContext dbContext, 
            IInitializeDbRepository initializeDbRepository,
            IArtisanRepository artisanRepository,
            ICustomerRepository customerRepository,
            IDisputeRepository disputeRepository,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            IRatingRepository ratingRepository,
            IServiceRepository serviceRepository,
            IUserRepository userRepository
            )
        {
            _dbContext = dbContext;
            Artisans = artisanRepository;
            Customers = customerRepository;
            Disputes = disputeRepository;
            Orders = orderRepository;
            Payments = paymentRepository;
            Ratings = ratingRepository;
            Services = serviceRepository;
            Users = userRepository;
            InitializeDb = initializeDbRepository;
        }

        public IInitializeDbRepository InitializeDb { get; }
        public IArtisanRepository Artisans { get; }
        public ICustomerRepository Customers { get; }
        public IDisputeRepository Disputes { get; }
        public IOrderRepository Orders { get; }
        public IPaymentRepository Payments { get; }
        public IRatingRepository Ratings { get; }
        public IServiceRepository Services { get; }
        public IUserRepository Users { get; }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}