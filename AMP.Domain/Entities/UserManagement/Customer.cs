using System.Collections.Generic;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.BusinessManagement;

namespace AMP.Domain.Entities.UserManagement
{
    public sealed class Customer : EntityBase
    {
        public string UserId { get; private set; }
        public User User { get; private set; }

        private readonly List<Rating> _ratings = new();
        public IEnumerable<Rating> Ratings => _ratings.AsReadOnly();
        
        private readonly List<Order> _orders = new();
        public IEnumerable<Order> Orders => _orders.AsReadOnly();

        private readonly List<Dispute> _disputes = new();
        public IEnumerable<Dispute> Disputes => _disputes.AsReadOnly();
        
        private readonly List<Payment> _payments = new();
        public IEnumerable<Payment> Payments => _payments.AsReadOnly();

        private readonly List<Request> _requests = new();
        public IEnumerable<Request> Requests => _requests.AsReadOnly();

        private Customer(){}

        private Customer(string userId)
        {
            UserId = userId;
        }

        public static Customer Create(string userId) 
            => new Customer(userId);

        public Customer ForUserId(string userId)
        {
            UserId = userId;
            return this;
        }
        
        public Customer ForUser(User user)
        {
            User = user;
            return this;
        }
    }
}