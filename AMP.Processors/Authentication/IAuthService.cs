using AMP.Processors.QueryObjects;

namespace AMP.Processors.Authentication
{
    public interface IAuthService
    {
        string GenerateToken(LoginQueryObject user);
    }
}