using System.Collections.Generic;
using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities
{
    public class Artisans : EntityBase
    {
        public int UserId { get; private set; }
        public string BusinessName { get; private set; }
        public string Description { get; private set; }
        public bool IsVerified { get; private set; }
        public bool IsApproved { get; private set; }
        public bool IsSuspended { get; private set; }
        public Users User { get; set; }

        private readonly List<Services> _services = new List<Services>();
        public IEnumerable<Services> Services => _services.AsReadOnly();

        private readonly List<Ratings> _ratings = new List<Ratings>();
        public IEnumerable<Ratings> Ratings => _ratings.AsReadOnly();

        private readonly List<Proposals> _proposals = new List<Proposals>();
        public IEnumerable<Proposals> Proposals => _proposals.AsReadOnly();

        private readonly List<Disputes> _disputes = new List<Disputes>();
        public IEnumerable<Disputes> Disputes => _disputes.AsReadOnly();

        private Artisans(){}

        private Artisans(int userId)
        {
            UserId = userId;
        }

        public Artisans Create(int userId)
        {
            return new Artisans(userId);
        }

        public Artisans ForUserId(int userId)
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

        public Artisans IsSuspendedd(bool isSuspended)
        {
            IsSuspended = isSuspended;
            return this;
        }
    }
}