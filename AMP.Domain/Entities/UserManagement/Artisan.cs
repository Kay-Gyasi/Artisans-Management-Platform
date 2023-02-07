using System;
using System.Collections.Generic;
using System.Linq;
using AMP.Domain.Entities.Base;
using AMP.Domain.Entities.BusinessManagement;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities.UserManagement
{
    public sealed class Artisan : Entity
    {
        public string UserId { get; private set; }
        public string BusinessName { get; private set; }
        public BusinessType Type { get; private set; }
        
        /// <summary>
        /// Energy Commission Certification Number (for electricians only)
        /// </summary>
        public string ECCN { get; private set; }
        public string Description { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsApproved { get; private set; }
        public User User { get; private set; }

        private readonly List<Order> _orders = new();
        public IEnumerable<Order> Orders => _orders.AsReadOnly();

        private readonly List<Service> _services = new();
        public IEnumerable<Service> Services => _services.AsReadOnly();

        private readonly List<Rating> _ratings = new();
        public IEnumerable<Rating> Ratings => _ratings.AsReadOnly();

        private readonly List<Request> _requests = new();
        public IEnumerable<Request> Requests => _requests.AsReadOnly();

        private Artisan(){}

        private Artisan(string userId)
        {
            UserId = userId;
        }

        public static Artisan Create(string userId) 
            => new Artisan(userId);

        public Artisan ForUserId(string userId)
        {
            UserId = userId;
            return this;
        }

        public Artisan ForUser(User user)
        {
            User = user;
            return this;
        }

        public Artisan WithBusinessName(string businessName)
        {
            BusinessName = businessName;
            return this;
        }
        
        public Artisan WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public Artisan IsVerifiedd(bool isVerified)
        {
            IsVerified = isVerified;
            return this;
        }

        public Artisan IsApprovedd(bool isApproved)
        {
            IsApproved = isApproved;
            return this;
        }

        public Artisan CreatedOn()
        {
            DateCreated = DateTime.UtcNow;
            return this;
        }

        public Artisan Offers(IEnumerable<Service> services)
        {
            _services.Clear();
            _services.AddRange(services);
            return this;
        }

        public Artisan OfType(BusinessType type)
        {
            Type = type;
            return this;
        }

        /// <summary>
        /// Sets the Energy Commission Certificate Number for the electrician.
        /// </summary>
        /// <param name="eccn"></param>
        /// <returns>This instance of Artisans.</returns>
        public Artisan HasEccn(string eccn)
        {
            if (_services.All(x => x.Name != "Electrical Works")) return this;
            ECCN = eccn;
            return this;
        }
    }
}