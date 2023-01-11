﻿namespace AMP.Processors.Commands.UserManagement
{
    public class UserCommand
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string? OtherName { get; set; }
        public string? DisplayName => string.Join(" ", new[] {FirstName, FamilyName});
        public string? MomoNumber { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsRemoved { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
        public LevelOfEducation LevelOfEducation { get; set; }
        public ContactCommand Contact { get; set; } = new();
        public AddressCommand Address { get; set; }
        public ImageCommand? Image { get; set; }
        public List<LanguagesCommand> Languages { get; set; }
    }
}