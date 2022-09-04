namespace AMP.Processors.Dtos
{
    public class RatingDto
    {
        public string Id { get; set; }
        public string ArtisanId { get; set; }
        public string CustomerId { get; set; }
        public int Votes { get; set; }
        public string Description { get; set; }
        public ArtisanDto Artisan { get; set; }
        public CustomerDto Customer { get; set; }
    }
}