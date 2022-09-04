using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        Task<string> GetIdByEmail(string email);
        Task<Users> Authenticate(SigninCommand command);
        (byte[], byte[]) Register(UserCommand command);
        (byte[], byte[]) Register(string password);
        Task<bool> Exists(string email);
    }
}