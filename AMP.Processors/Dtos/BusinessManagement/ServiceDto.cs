using AMP.Processors.Dtos.UserManagement;

namespace AMP.Processors.Dtos.BusinessManagement
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