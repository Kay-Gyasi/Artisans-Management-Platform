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
        private readonly IInitializeDbRepository _initializeDbRepository;
        private readonly IArtisanRepository _artisanRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IDisputeRepository _disputeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(AmpDbContext dbContext, 
            IInitializeDbRepository initializeDbRepository,
            IArtisanRepository artisanRepository,
            ICustomerRepository customerRepository,
            IDisputeRepository disputeRepository,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            IRatingRepository ratingRepository,
            IServiceRepository serviceRepository,
            ILanguageRepository languageRepository,
            IUserRepository userRepository
            )
        {
            _dbContext = dbContext;
            _initializeDbRepository = initializeDbRepository;
            _artisanRepository = artisanRepository;
            _customerRepository = customerRepository;
            _disputeRepository = disputeRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _ratingRepository = ratingRepository;
            _serviceRepository = serviceRepository;
            _languageRepository = languageRepository;
            _userRepository = userRepository;
        }

        public IInitializeDbRepository InitializeDb => _initializeDbRepository;
        public IArtisanRepository Artisans => _artisanRepository;
        public ICustomerRepository Customers => _customerRepository;
        public IDisputeRepository Disputes => _disputeRepository;
        public IOrderRepository Orders => _orderRepository;
        public IPaymentRepository Payments => _paymentRepository;
        public IRatingRepository Ratings => _ratingRepository;
        public IServiceRepository Services => _serviceRepository;
        public IUserRepository Users => _userRepository;
        public ILanguageRepository Languages => _languageRepository;

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}