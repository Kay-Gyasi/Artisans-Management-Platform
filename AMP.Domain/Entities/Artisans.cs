using System;
using System.Collections.Generic;
using System.Linq;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;

namespace AMP.Domain.Entities
{
    public sealed class Artisans : EntityBase
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
        public Users User { get; private set; }

        private readonly List<Orders> _orders = new();
        public IEnumerable<Orders> Orders => _orders.AsReadOnly();

        private readonly List<Services> _services = new();
        public IEnumerable<Services> Services => _services.AsReadOnly();

        private readonly List<Ratings> _ratings = new();
        public IEnumerable<Ratings> Ratings => _ratings.AsReadOnly();

        private readonly List<Requests> _requests = new();
        public IEnumerable<Requests> Requests => _requests.AsReadOnly();

        private Artisans(){}

        private Artisans(string userId)
        {
            UserId = userId;
        }

        public static Artisans Create(string userId) 
            => new Artisans(userId);

        public Artisans ForUserId(string userId)
        {
            UserId = userId;
            return this;
        }

        public Artisans ForUser(Users user)
        {
            User = user;
            return this;
        }

        public Artisans WithBusinessName(string businessName)
        {
            BusinessName = businessName;
            return this;
        }
        
        public Artisans WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public Artisans IsVerifiedd(bool isVerified)
        {
            IsVerified = isVerified;
            return this;
        }

        public Artisans IsApprovedd(bool isApproved)
        {
            IsApproved = isApproved;
            return this;
        }

        public Artisans CreatedOn()
        {
            DateCreated = DateTime.UtcNow;
            return this;
        }

        public Artisans Offers(IEnumerable<Services> services)
        {
            _services.Clear();
            _services.AddRange(services);
            return this;
        }

        public Artisans OfType(BusinessType type)
        {
            Type = type;
            return this;
        }

        /// <summary>
        /// Sets the Energy Commission Certificate Number for the electrician.
        /// </summary>
        /// <param name="eccn"></param>
        /// <returns>This instance of Artisans.</returns>
        public Artisans HasEccn(string eccn)
        {
            if (_services.All(x => x.Name != "Electrical Works")) return this;
            ECCN = eccn;
            return this;
        }
    }
}