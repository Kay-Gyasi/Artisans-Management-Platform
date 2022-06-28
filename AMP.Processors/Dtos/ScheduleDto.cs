using System;

namespace AMP.Processors.Dtos
{
    public class ScheduleDto
    {
        public int OrderId { get; set; }
        public int ArtisanId { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
        public OrderDto Order { get; set; }
        public ArtisanDto Artisan { get; set; }
    }
}