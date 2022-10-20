using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;
using AMP.Processors.Commands;

namespace AMP.Processors.PageDtos
{
    public class UserPageDto
    {
        public string Id { get; set; }
        public string UserNo { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string OtherName { get; set; }
        public string DisplayName { get; set; }
        public string MomoNumber { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsRemoved { get; set; }
        public UserType Type { get; set; }
        public LevelOfEducation LevelOfEducation { get; set; }
        public ContactCommand Contact { get; set; }
        public AddressCommand Address { get; set; }
        public ImagePageDto Image { get; set; }
    }
}