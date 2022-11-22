using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.QueryObjects;

namespace AMP.Processors.Authentication
{
    public interface IAuthService
    {
        string GenerateToken(LoginQueryObject user);
    }
}