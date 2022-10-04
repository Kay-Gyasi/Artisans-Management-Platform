using System.Threading.Tasks;
using AMP.Processors.Interfaces;
using AMP.Processors.Repositories.Administration;

namespace AMP.Processors.Repositories.UoW
{
    public interface IUnitOfWork
    {
        public IInitializeDbRepository InitializeDb { get; }
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

        Task<bool> SaveChangesAsync();
    }
}