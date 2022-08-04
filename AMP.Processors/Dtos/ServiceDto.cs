using System.Collections.Generic;

namespace AMP.Processors.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ArtisanDto> Artisans { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}