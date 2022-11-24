namespace AMP.Processors.PageDtos
{
    public class ArtisanPageDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string BusinessName { get; set; }
        public BusinessType Type { get; set; }
        
        /// <summary>
        /// Energy Commission Certification Number (for electricians only)
        /// </summary>
        public string Eccn { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsVerified { get; set; }
        public bool IsApproved { get; set; }
        public double Rating { get; set; } 
        public UserPageDto User { get; set; }
    }
}