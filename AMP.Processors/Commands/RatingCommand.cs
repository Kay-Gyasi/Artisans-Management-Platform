namespace AMP.Processors.Commands
{
    public class RatingCommand
    {
        public string Id { get; set; }
        public string ArtisanId { get; set; }
        public string UserId { get; set; }
        public int Votes { get; set; }
        public string Description { get; set; }
    }
}