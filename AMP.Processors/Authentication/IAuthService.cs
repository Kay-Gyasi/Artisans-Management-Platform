using AMP.Domain.Entities;
using AMP.Processors.Commands;

namespace AMP.Processors.Authentication
{
    public interface IAuthService
    {
        string GenerateToken(Users user);
    }
}