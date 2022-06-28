using System.Collections.Generic;
using AMP.Domain.Enums;
using AMP.Domain.ValueObjects;

namespace AMP.Processors.Commands
{
    public class UserCommand
    {
        public string UserNo { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string OtherName { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public string MomoNumber { get; set; }
        public LevelOfEducation LevelOfEducation { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public List<int> Languages { get; set; } // get values from immutable array
    }
}