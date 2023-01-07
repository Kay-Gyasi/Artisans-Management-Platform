using AMP.Processors.Commands.BusinessManagement;

namespace AMP.Processors.Commands.UserManagement
{
    public class ArtisanCommand
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public BusinessType Type { get; set; }
        
        /// <summary>
        /// Energy Commission Certification Number (for electricians only)
        /// </summary>
        public string Eccn { get; set; }

        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public List<ServiceCommand> Services { get; set; } // Ids of services
    }
}