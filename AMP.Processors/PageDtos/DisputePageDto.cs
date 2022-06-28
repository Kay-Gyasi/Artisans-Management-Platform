using AMP.Domain.Enums;

namespace AMP.Processors.PageDtos
{
    public class DisputePageDto
    {
        public int CustomerId { get; set; }
        public int ArtisanId { get; set; }
        public string Details { get; set; }
        public DisputeStatus Status { get; set; }
        public CustomerPageDto Customer { get; set; }
        public ArtisanPageDto Artisan { get; set; }
    }
}