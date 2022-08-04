namespace AMP.Processors.PageDtos
{
    public class RatingPageDto
    {
        public int Id { get; set; }
        public int ArtisanId { get; set; }
        public int CustomerId { get; set; }
        public int Votes { get; set; }
        public string Description { get; set; }
        public ArtisanPageDto Artisan { get; set; }
        public CustomerPageDto Customer { get; set; }
    }
}