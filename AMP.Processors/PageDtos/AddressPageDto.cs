using AMP.Domain.Enums;

namespace AMP.Processors.PageDtos
{
    public class AddressPageDto
    {
        public Countries Country { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
    }

    public class ContactPageDto
    {
        public string EmailAddress { get; set; }
        public string PrimaryContact { get; set; }
        public string PrimaryContact2 { get; set; }
        public string PrimaryContact3 { get; set; }
    }
}