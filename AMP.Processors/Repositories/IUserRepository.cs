using System.Data.Common;
using AMP.Processors.QueryObjects;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IUserRepository : IRepositoryBase<Users>
    {
        /// <summary>
        /// Returns user id.
        /// </summary>
        /// <param name="phone">Phone number of user being queried for</param>
        /// <param name="transaction">The underlying transaction being used for this operation</param>
        /// <param name="connection">The underlying database connection being used for this operation</param>
        /// <remarks>
        /// Using dapper for this operation to prevent irrelevant querying
        /// </remarks>
        Task<string> GetIdByPhone(string phone, DbTransaction transaction = null, DbConnection connection = null);
        Task<LoginQueryObject> Authenticate(SigninCommand command);
        Task<LoginQueryObject> GetUserInfoForRefreshToken(string id);
        (byte[], byte[]) Register(UserCommand command);
        (byte[], byte[]) Register(string password);
        Task<bool> Exists(string phone);
        Task<Users> GetByPhone(string phone);
        Task<Users> GetByPhoneAndConfirmCode(string phone, string confirmCode);
    }
}