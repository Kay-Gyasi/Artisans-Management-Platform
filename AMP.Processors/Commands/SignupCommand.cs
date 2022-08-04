using AMP.Domain.Enums;

namespace AMP.Processors.Commands
{
    public class SignupCommand
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string OtherName { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
    }

    public class SigninCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SigninResponse
    {
        public string Token { get; set; }
    }
}