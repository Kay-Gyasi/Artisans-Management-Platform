namespace AMP.Processors.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int ArtisanId { get; set; }
        public int CustomerId { get; set; }
        public int Votes { get; set; }
        public string Description { get; set; }
        public ArtisanDto Artisan { get; set; }
        public CustomerDto Customer { get; set; }
    }
}