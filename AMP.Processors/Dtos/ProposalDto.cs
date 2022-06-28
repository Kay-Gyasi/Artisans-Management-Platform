namespace AMP.Processors.Dtos
{
    public class ProposalDto
    {
        public int OrderId { get; set; }
        public int ArtisanId { get; set; }
        public bool IsAccepted { get; set; }
        public OrderDto Order { get; set; }
        public ArtisanDto Artisan { get; set; }
    }
}