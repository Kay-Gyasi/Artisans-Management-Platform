using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public sealed class Customers : EntityBase
    {
        public string UserId { get; private set; }
        public Users User { get; private set; }

        private readonly List<Ratings> _ratings = new();
        public IEnumerable<Ratings> Ratings => _ratings.AsReadOnly();
        
        private readonly List<Orders> _orders = new();
        public IEnumerable<Orders> Orders => _orders.AsReadOnly();

        private readonly List<Disputes> _disputes = new();
        public IEnumerable<Disputes> Disputes => _disputes.AsReadOnly();
        
        private readonly List<Payments> _payments = new();
        public IEnumerable<Payments> Payments => _payments.AsReadOnly();

        private readonly List<Requests> _requests = new();
        public IEnumerable<Requests> Requests => _requests.AsReadOnly();

        private Customers(){}

        private Customers(string userId)
        {
            UserId = userId;
        }

        public static Customers Create(string userId) 
            => new Customers(userId);

        public Customers ForUserId(string userId)
        {
            UserId = userId;
            return this;
        }
        
        public Customers ForUser(Users user)
        {
            User = user;
            return this;
        }
    }
}