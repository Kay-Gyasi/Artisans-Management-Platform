namespace AMP.Processors.Dtos
{
    public class ServiceDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ArtisanDto> Artisans { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}