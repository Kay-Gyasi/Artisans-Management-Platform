using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.UserManagement;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Domain.Entities.BusinessManagement
{
    public sealed class Order : EntityBase
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
        public Artisan Artisan { get; private set; }
        public Address WorkAddress { get; private set; }
        public Customer Customer { get; private set; }
        public Service Service { get; private set; }

        private readonly List<Dispute> _disputes = new();
        public IEnumerable<Dispute> Disputes => _disputes.AsReadOnly();
        
        private readonly List<Payment> _payments = new();
        public IEnumerable<Payment> Payments => _payments.AsReadOnly();

        private readonly List<Request> _requests = new();
        public IEnumerable<Request> Requests => _requests.AsReadOnly();

        private Order() {}

        private Order(string customerId, string serviceId)
        {
            CustomerId = customerId;
            ServiceId = serviceId;
        }

        public static Order Create(string customerId, string serviceId) 
            => new Order(customerId, serviceId);

        public Order ForCustomerWithId(string customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Order ForServiceWithId(string serviceId)
        {
            ServiceId = serviceId;
            return this;
        }

        public Order ForArtisanWithId(string? artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Order IsCompleted(bool isCompleted)
        {
            IsComplete = isCompleted;
            return this;
        }
        
        public Order IsArtisanCompleted(bool isCompleted)
        {
            IsArtisanComplete = isCompleted;
            return this;
        }

        public Order WithDescription(string description)
        {
            Description = description;
            return this;
        }
        
        public Order WithReferenceNo(string referenceNo)
        {
            ReferenceNo = referenceNo;
            return this;
        }

        public Order WithCost(decimal cost)
        {
            Cost = cost;
            return this;
        }

        public Order WithUrgency(Urgency urgency)
        {
            Urgency = urgency;
            return this;
        }

        public Order WithStatus(OrderStatus status)
        {
            Status = status;
            return this;
        }
        
        public Order WithScope(ScopeOfWork scope)
        {
            Scope = scope;
            return this;
        }

        public Order WithPreferredStartDate(DateTime date)
        {
            if (date < DateTime.UtcNow)
            {
                PreferredStartDate = DateTime.UtcNow.AddDays(1);
                return this;
            }

            PreferredStartDate = date;
            return this;
        }
        
        public Order WithPreferredCompletionDate(DateTime date)
        {
            if (date < PreferredStartDate)
            {
                PreferredCompletionDate = DateTime.UtcNow.AddDays(1);
                return this;
            }
            PreferredCompletionDate = date; // make sure date is not past
            return this;
        }

        public Order WithWorkAddress(Address address)
        {
            WorkAddress = address;
            return this;
        }

        public Order ForCustomer(Customer customer)
        {
            Customer = customer;
            return this;
        }

        public Order OnService(Service service)
        {
            Service = service;
            return this;
        }

        public Order RequestAccepted(bool isRequestAccepted)
        {
            IsRequestAccepted = isRequestAccepted;
            return this;
        }
    }
}