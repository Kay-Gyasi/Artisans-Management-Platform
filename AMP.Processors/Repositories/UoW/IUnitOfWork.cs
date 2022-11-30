﻿using System.Data.Common;

namespace AMP.Processors.Repositories.UoW
{
    public interface IUnitOfWork
    {
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

        Task<bool> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        IExecutionStrategy GetExecutionStrategy();
        DbConnection GetDbConnection();
    }
}