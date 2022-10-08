using System.Threading.Tasks;
using AMP.Persistence.Database;
using AMP.Processors.Interfaces;
using AMP.Processors.Interfaces.UoW;
using AMP.Processors.Repositories;
using AMP.Processors.Repositories.Administration;

namespace AMP.Persistence.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AmpDbContext _dbContext;

        public UnitOfWork(AmpDbContext dbContext, 
            IArtisanRepository artisanRepository,
            ICustomerRepository customerRepository,
            IDisputeRepository disputeRepository,
            IOrderRepository orderRepository,
            IPaymentRepository paymentRepository,
            IRatingRepository ratingRepository,
            IServiceRepository serviceRepository,
            ILanguageRepository languageRepository,
            IUserRepository userRepository,
            IImageRepository imageRepository,
            IRequestRepository requestRepository)
        {
            _dbContext = dbContext;
            Artisans = artisanRepository;
            Customers = customerRepository;
            Disputes = disputeRepository;
            Orders = orderRepository;
            Payments = paymentRepository;
            Ratings = ratingRepository;
            Services = serviceRepository;
            Languages = languageRepository;
            Users = userRepository;
            Images = imageRepository;
            Requests = requestRepository;
        }
        
        public IArtisanRepository Artisans { get; }

        public ICustomerRepository Customers { get; }

        public IDisputeRepository Disputes { get; }

        public IOrderRepository Orders { get; }

        public IPaymentRepository Payments { get; }

        public IRatingRepository Ratings { get; }

        public IServiceRepository Services { get; }

        public IUserRepository Users { get; }

        public ILanguageRepository Languages { get; }

        public IImageRepository Images { get; }

        public IRequestRepository Requests { get; }

        public async Task<bool> SaveChangesAsync() 
            => await _dbContext.SaveChangesAsync() > 0;
    }
}