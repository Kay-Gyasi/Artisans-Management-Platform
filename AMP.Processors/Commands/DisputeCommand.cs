using AMP.Domain.Enums;

namespace AMP.Processors.Commands
{
    public class DisputeCommand
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DisputeStatus Status { get; set; }
        public int ArtisanId { get; set; }
        public string Details { get; set; }
    }
}