using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Domain.Entities
{
    public class Orders : EntityBase
    {
        public int CustomerId { get; private set; }
        public int? ArtisanId { get; private set; }
        public bool IsComplete { get; private set; }
        public bool IsRequestAccepted { get; private set; }
        public int ServiceId { get; private set; }
        public int? PaymentId { get; private set; }
        public string Description { get; private set; }
        public decimal Cost { get; private set; } // To be set by approved artisan
        public Urgency Urgency { get; private set; }
        public ScopeOfWork Scope { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime PreferredStartDate { get; private set; }
        public DateTime PreferredCompletionDate { get; private set; }
        public Artisans Artisan { get; private set; }
        public Address WorkAddress { get; private set; }
        public Customers Customer { get; private set; }
        public Services Service { get; private set; }
        public Payments Payment { get; private set; }

        private readonly List<Disputes> _disputes = new List<Disputes>();
        public IEnumerable<Disputes> Disputes => _disputes.AsReadOnly();

        private readonly List<Requests> _requests = new List<Requests>();
        public IEnumerable<Requests> Requests => _requests.AsReadOnly();

        private Orders() {}

        private Orders(int customerId, int serviceId)
        {
            CustomerId = customerId;
            ServiceId = serviceId;
        }

        public static Orders Create(int customerId, int serviceId)
        {
            return new Orders(customerId, serviceId);
        }

        public Orders ForCustomerWithId(int customerId)
        {
            CustomerId = customerId;
            return this;
        }

        public Orders ForServiceWithId(int serviceId)
        {
            ServiceId = serviceId;
            return this;
        }

        public Orders ForArtisanWithId(int? artisanId)
        {
            ArtisanId = artisanId;
            return this;
        }

        public Orders IsCompleted(bool isCompleted)
        {
            IsComplete = isCompleted;
            return this;
        }

        public Orders WithPaymentId(int? paymentId)
        {
            PaymentId = paymentId;
            return this;
        }

        public Orders WithDescription(string description)
        {
            Description = description;
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

        public Orders WithPayment(Payments payment)
        {
            Payment = payment;
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