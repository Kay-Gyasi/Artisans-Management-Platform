using System.Collections.Generic;

namespace AMP.Processors.Commands
{
    public class ArtisanCommand
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public List<ServiceCommand> Services { get; set; } // Ids of services
    }
}