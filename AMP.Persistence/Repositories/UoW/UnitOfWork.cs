﻿using System.Data.Common;
using AMP.Processors.Repositories.UoW;
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
            IInvitationRepository invitationRepository)
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

        public IDbContextTransaction BeginTransaction() => _dbContext.Database.BeginTransaction();
        public IExecutionStrategy GetExecutionStrategy() => _dbContext.Database.CreateExecutionStrategy();
        public DbConnection GetDbConnection() => _dbContext.Database.GetDbConnection();
        public AmpDbContext GetDbContext() => _dbContext;

        public async Task<bool> SaveChangesAsync() 
            => await _dbContext.SaveChangesAsync() > 0;
    }
}