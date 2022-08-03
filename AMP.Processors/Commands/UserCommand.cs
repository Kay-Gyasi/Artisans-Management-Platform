using System.Collections.Generic;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.Commands
{
    public class UserCommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string OtherName { get; set; }
        public string DisplayName => string.Join(" ", new[] {FirstName, FamilyName});
        public string ImageUrl { get; set; }
        public string MomoNumber { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsRemoved { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public LevelOfEducation LevelOfEducation { get; set; }
        public ContactCommand Contact { get; set; }
        public AddressCommand Address { get; set; }
        public List<LanguagesCommand> Languages { get; set; } 
    }
}