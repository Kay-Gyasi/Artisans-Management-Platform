using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        Task<string> GetIdByPhone(string phone);
        Task<Users> Authenticate(SigninCommand command);
        (byte[], byte[]) Register(UserCommand command);
        (byte[], byte[]) Register(string password);
        Task<bool> Exists(string phone);
        Task<Users> GetByPhone(string phone);
        Task<Users> GetByPhoneAndConfirmCode(string phone, string confirmCode);
    }
}