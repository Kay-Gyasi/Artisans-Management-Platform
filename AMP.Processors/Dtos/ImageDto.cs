namespace AMP.Processors.Dtos
{
    public class ImageDto
    {
        public string Id { get; set; }
        public string? UserId { get; set; }
        public string PublicId { get; set; }
        public string ImageUrl { get; set; }
        public UserDto User { get; set; }
    }
}