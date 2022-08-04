using System.Collections.Generic;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string OtherName { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public string MomoNumber { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsRemoved { get; set; }
        public UserType Type { get; set; }
        public LevelOfEducation LevelOfEducation { get; set; }
        public ContactDto Contact { get; set; }
        public AddressDto Address { get; set; }
        public List<LanguagesDto> Languages { get; set; }
        public List<ArtisanDto> Artisans { get; set; }
        public List<CustomerDto> Customers { get; set; }
    }
}