using AMP.Domain.Entities.Base;

namespace AMP.Domain.Entities.UserManagement
{
    public sealed class Image : Entity
    {
        public string? UserId { get; private set; }
        public string PublicId { get; private set; }
        public string ImageUrl { get; private set; }
        public User User { get; private set; }

        private Image(string imageUrl, string publicId)
        {
            ImageUrl = imageUrl;
            PublicId = publicId;
        }

        public static Image Create(string imageUrl, string publicId) 
            => new Image(imageUrl, publicId);

        public Image WithUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
            return this;
        }

        public Image WithPublicId(string publicId)
        {
            PublicId = publicId;
            return this;
        }

        public Image ForUserWithId(string? userId)
        {
            UserId = userId;
            return this;
        }
    }
}