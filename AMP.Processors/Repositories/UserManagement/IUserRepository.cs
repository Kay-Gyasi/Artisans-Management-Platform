using System.Data.Common;
using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.QueryObjects;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement
{
    public interface IUserRepository : IRepository<User>
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
        Task<User> GetByPhone(string phone);
        Task<User> GetByPhoneAndConfirmCode(string phone, string confirmCode);
        Task<List<UserLookup>> GetLookupAsync(string term, string type);
        Task<IEnumerable<User>> GetAllNotPaymentCustomers();
    }
}