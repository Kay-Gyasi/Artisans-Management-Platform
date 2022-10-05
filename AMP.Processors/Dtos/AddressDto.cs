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
}