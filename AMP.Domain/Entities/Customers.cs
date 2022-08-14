using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Customers : EntityBase
    {
        public int UserId { get; private set; }
        public Users User { get; private set; }

        private readonly List<Ratings> _ratings = new List<Ratings>();
        public IEnumerable<Ratings> Ratings => _ratings.AsReadOnly();
        
        private readonly List<Orders> _orders = new List<Orders>();
        public IEnumerable<Orders> Orders => _orders.AsReadOnly();

        private readonly List<Disputes> _disputes = new List<Disputes>();
        public IEnumerable<Disputes> Disputes => _disputes.AsReadOnly();
        
        private readonly List<Payments> _payments = new List<Payments>();
        public IEnumerable<Payments> Payments => _payments.AsReadOnly();

        private readonly List<Requests> _requests = new List<Requests>();
        public IEnumerable<Requests> Requests => _requests.AsReadOnly();

        private Customers(){}

        private Customers(int userId)
        {
            UserId = userId;
        }

        public static Customers Create(int userId)
        {
            return new Customers(userId);
        }

        public Customers ForUserId(int userId)
        {
            UserId = userId;
            return this;
        }
        
        public Customers ForUser(Users user)
        {
            User = user;
            return this;
        }

        public Customers CreatedOn(DateTime date)
        {
            DateCreated = date;
            return this;
        }

        public Customers WithId(int id)
        {
            Id = id;
            return this;
        }
    }
}