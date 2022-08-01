using AMP.Domain.Enums;

namespace AMP.Processors.Dtos
{
    public class AddressDto
    {
            public Countries Country { get; set; }
            public string City { get; set; }
            public string Town { get; set; }
            public string StreetAddress { get; set; }
            public string StreetAddress2 { get; set; }
    }

    public class ContactDto
    {
        public string EmailAddress { get; set; }
        public string PrimaryContact { get; set; }
        public string PrimaryContact2 { get; set; }
        public string PrimaryContact3 { get; set; }
    }
}