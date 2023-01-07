using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace AMP.Processors.Commands.UserManagement
{

    public class UploadImageCommand
    {
        public string Name { get; set; } = "file";
        public StreamContent Content { get; set; }
        public string FileName { get; set; }
    }

    public class AddressCommand
    {
        public Countries Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
    }

    public class ContactCommand
    {
        public string EmailAddress { get; set; }
        public string PrimaryContact { get; set; }
        public string PrimaryContact2 { get; set; }
        public string PrimaryContact3 { get; set; }
    }

    public class ImageCommand
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PublicId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile Image { get; set; }
    }
}