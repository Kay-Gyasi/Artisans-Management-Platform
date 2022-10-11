using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Domain.Entities
{
    public sealed class Orders : EntityBase
    {
        public string ReferenceNo { get; private set; }
        public string CustomerId { get; private set; }
        public string? ArtisanId { get; private set; }
        public bool IsComplete { get; private set; }
        public bool IsArtisanComplete { get; private set; }
        public bool IsRequestAccepted { get; private set; }
        public string ServiceId { get; private set; }
        public string Description { get; private set; }
        public decimal Cost { get; private set; }
        public decimal PaymentMade { get; private set; }
        public Urgency Urgency { get; private set; }
        public ScopeOfWork Scope { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime PreferredStartDate { get; private set; }
        public DateTime PreferredCompletionDate { get; private set; }
        public Artisans Artisan { get; private set; }
        public Address WorkAddress { get; private set; }
        public Customers Customer { get; private set; }
        public Services Service { get; private set; }

        private readonly List<Disputes> _disputes = new();
        public IEnumerable<Disputes> Disputes => _disputes.AsReadOnly();
        
        private readonly List<Payments> _payments = new();
        public IEnumerable<Payments> Payments => _payments.AsReadOnly();

        private readonly List<Requests> _requests = new();
        public IEnumerable<Requests> Requests => _requests.AsReadOnly();

        private Orders() {}

        private Orders(string customerId, string serviceId)
        {
            CustomerId = customerId;
            ServiceId = serviceId;
        }

        public static Orders Create(string customerId, string serviceId) 
            => new Orders(customerId, serviceId);

        public Orders ForCustomerWithId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Orders ForServiceWithId(string serviceId)
        {
            ServiceId = serviceId;
            return this;
        }

        public Orders ForArtisanWithId(string? artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Orders IsCompleted(bool isCompleted)
        {
            IsComplete = isCompleted;
            return this;
        }
        
        public Orders IsArtisanCompleted(bool isCompleted)
        {
            IsArtisanComplete = isCompleted;
            return this;
        }

        public Orders WithDescription(string description)
        {
            Description = description;
            return this;
        }
        
        public Orders WithReferenceNo(string referenceNo)
        {
            ReferenceNo = referenceNo;
            return this;
        }

        public Orders WithCost(decimal cost)
        {
            Cost = cost;
            return this;
        }

        public Orders WithUrgency(Urgency urgency)
        {
            Urgency = urgency;
            return this;
        }

        public Orders WithStatus(OrderStatus status)
        {
            Status = status;
            return this;
        }
        
        public Orders WithScope(ScopeOfWork scope)
        {
            Scope = scope;
            return this;
        }

        public Orders WithPreferredStartDate(DateTime date)
        {
            if (date < DateTime.UtcNow)
            {
                PreferredStartDate = DateTime.UtcNow.AddDays(1);
                return this;
            }

            PreferredStartDate = date;
            return this;
        }
        
        public Orders WithPreferredCompletionDate(DateTime date)
        {
            if (date < PreferredStartDate)
            {
                PreferredCompletionDate = DateTime.UtcNow.AddDays(1);
                return this;
            }
            PreferredCompletionDate = date; // make sure date is not past
            return this;
        }

        public Orders WithWorkAddress(Address address)
        {
            WorkAddress = address;
            return this;
        }

        public Orders ForCustomer(Customers customer)
        {
            Customer = customer;
            return this;
        }

        public Orders OnService(Services service)
        {
            Service = service;
            return this;
        }

        public Orders RequestAccepted(bool isRequestAccepted)
        {
            IsRequestAccepted = isRequestAccepted;
            return this;
        }

        public Orders CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }
    }
}