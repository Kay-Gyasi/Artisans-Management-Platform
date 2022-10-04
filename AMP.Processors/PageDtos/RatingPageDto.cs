namespace AMP.Processors.PageDtos
{
    public class RatingPageDto
    {
        public string Id { get; set; }
        public string ArtisanId { get; set; }
        public string CustomerId { get; set; }
        public int Votes { get; set; }
        public string Description { get; set; }
        public ArtisanPageDto Artisan { get; set; }
        public CustomerPageDto Customer { get; set; }
    }
}