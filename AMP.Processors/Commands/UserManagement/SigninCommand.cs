namespace AMP.Processors.Commands.UserManagement
{
    public class SigninCommand
    {
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class SigninResponse
    {
        public string Token { get; set; }
    }
}