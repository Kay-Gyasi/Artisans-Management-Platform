using System;
using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Artisans : EntityBase
    {
        public string UserId { get; private set; }
        public string BusinessName { get; private set; }
        public string Description { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsApproved { get; private set; }
        public Users User { get; private set; }

        private readonly List<Orders> _orders = new List<Orders>();
        public IEnumerable<Orders> Orders => _orders.AsReadOnly();

        private readonly List<Services> _services = new List<Services>();
        public IEnumerable<Services> Services => _services.AsReadOnly();

        private readonly List<Ratings> _ratings = new List<Ratings>();
        public IEnumerable<Ratings> Ratings => _ratings.AsReadOnly();

        private readonly List<Requests> _requests = new List<Requests>();
        public IEnumerable<Requests> Requests => _requests.AsReadOnly();

        private Artisans(){}

        private Artisans(string userId)
        {
            UserId = userId;
        }

        public static Artisans Create(string userId)
        {
            return new Artisans(userId);
        }

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
        
        public Artisans LastModifiedOn()
        {
            DateModified = DateTime.UtcNow;
            return this;
        }

        public Artisans Offers(List<Services> services)
        {
            _services.Clear();
            _services.AddRange(services);
            return this;
        }

        public Artisans WithId(string id)
        {
            Id = id;
            return this;
        }
    }
}