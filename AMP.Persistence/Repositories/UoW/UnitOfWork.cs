using System.Data.Common;
using AMP.Processors.Repositories.BusinessManagement;
using AMP.Processors.Repositories.Messaging;
using AMP.Processors.Repositories.UoW;
using AMP.Processors.Repositories.UserManagement;
using Microsoft.EntityFrameworkCore.Storage;

namespace AMP.Persistence.Repositories.Uow
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
            IRequestRepository requestRepository,
            IRegistrationRepository registrationRepository,
            IInvitationRepository invitationRepository,
            IChatMessageRepository chatMessageRepository,
            IConversationRepository conversationRepository,
            IConnectRequestRepository connectRequestRepository,
            IPaymentWithdrawalRepository paymentWithdrawalRepository,
            INotificationRepository notificationRepository)
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
            Registrations = registrationRepository;
            Invitations = invitationRepository;
            ChatMessages = chatMessageRepository;
            Conversations = conversationRepository;
            ConnectRequests = connectRequestRepository;
            Notifications = notificationRepository;
            PaymentWithdrawals = paymentWithdrawalRepository;
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
        public IRegistrationRepository Registrations { get; }
        public IInvitationRepository Invitations { get; }
        public IChatMessageRepository ChatMessages { get; }
        public IConnectRequestRepository ConnectRequests { get; }
        public INotificationRepository Notifications { get; }
        public IConversationRepository Conversations { get; }
        public IPaymentWithdrawalRepository PaymentWithdrawals { get; }

        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();
        public IExecutionStrategy GetExecutionStrategy() => _dbContext.Database.CreateExecutionStrategy();
        public DbConnection GetDbConnection() => _dbContext.Database.GetDbConnection();

        public async Task<bool> SaveChangesAsync() 
            => await _dbContext.SaveChangesAsync() > 0;
    }
}