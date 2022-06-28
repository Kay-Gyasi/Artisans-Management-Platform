using System.Collections.Generic;
using AMP.Processors.Dtos;

namespace AMP.Processors.Commands
{
    public class ArtisanCommand
    {
        public int UserId { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public List<int> Services { get; set; } // Ids of services
    }
}