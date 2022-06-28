using AMP.Domain.Enums;

namespace AMP.Processors.Dtos
{
    public class DisputeDto
    {
        public int CustomerId { get; set; }
        public int ArtisanId { get; set; }
        public string Details { get; set; }
        public DisputeStatus Status { get; set; }
        public CustomerDto Customer { get; set; }
        public ArtisanDto Artisan { get; set; }
    }
}