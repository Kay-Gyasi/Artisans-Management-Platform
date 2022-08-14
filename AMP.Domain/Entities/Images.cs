using AMP.Domain.Entities.Base;
using Microsoft.AspNetCore.Http;

namespace AMP.Domain.Entities
{
    public class Images : EntityBase
    {
        public int Id { get; private set; }
        public int? UserId { get; private set; }
        public string PublicId { get; private set; }
        public string ImageUrl { get; private set; }
        public Users User { get; private set; }

        private Images(string imageUrl, string publicId)
        {
            ImageUrl = imageUrl;
            PublicId = publicId;
        }

        public static Images Create(string imageUrl, string publicId)
        {
            return new Images(imageUrl, publicId);
        }

        public Images WithUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
            return this;
        }

        public Images WithPublicId(string publicId)
        {
            PublicId = publicId;
            return this;
        }

        public Images ForUserWithId(int? userId)
        {
            UserId = userId;
            return this;
        }
    }
}