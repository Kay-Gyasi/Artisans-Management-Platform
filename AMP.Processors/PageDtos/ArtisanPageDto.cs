using System.Collections.Generic;

namespace AMP.Processors.PageDtos
{
    public class ArtisanPageDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public double Rating { get; set; } // TODO:: Generate this in processor
        public UserPageDto User { get; set; }
    }
}