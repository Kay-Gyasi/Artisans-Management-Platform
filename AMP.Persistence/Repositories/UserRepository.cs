using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;
using AMP.Processors.Exceptions;
using AMP.Processors.Processors.Helpers;
using AMP.Processors.QueryObjects;
using AMP.Processors.Repositories.Base;
using Dapper;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        private readonly IDapperContext _dapperContext;

        public UserRepository(AmpDbContext context, ILogger<Users> logger,
            IDapperContext dapperContext) : base(context, logger)
        {
            _dapperContext = dapperContext;
        }

        /// <summary>
        /// Returns user id.
        /// </summary>
        /// <param name="phone">Phone number of user being queried for</param>
        /// <param name="transaction">The underlying transaction being used for this operation</param>
        /// <param name="connection">The underlying database connection being used for this operation</param>
        /// <remarks>
        /// Using dapper for this operation to prevent irrelevant querying
        /// </remarks>
        public async Task<string> GetIdByPhone(string phone, DbTransaction transaction = null, DbConnection connection = null)
        {
            if(connection is not null) return await _dapperContext.GetAsync<string>($"SELECT Id FROM Users WHERE Contact_PrimaryContact = '{phone}'"
                    .AddBaseFilterToWhereClause(),
                null, transaction, connection, CommandType.Text);
            return await _dapperContext.GetAsync<string>($"SELECT Id FROM Users WHERE Contact_PrimaryContact = '{phone}'"
                    .AddBaseFilterToWhereClause(),
                null, CommandType.Text);
        }

        // NB: Not doing async/await here cause I don't want to switch threads here and I want the
        // calling method to do the execution of the task. And because it is safe to do so here because 
        // I am not trying to catch any exceptions here and also I am not using a using statement
        public Task<bool> Exists(string phone) 
            => base.GetBaseQuery().AnyAsync(x => x.Contact.PrimaryContact == phone);

        public Task<Users> GetByPhone(string phone) 
            => GetBaseQuery().FirstOrDefaultAsync(x => x.Contact.PrimaryContact == phone);

        public async Task<Users> GetByPhoneAndConfirmCode(string phone, string confirmCode)
        {
            var user = await GetBaseQuery().FirstOrDefaultAsync(x =>
                x.Contact.PrimaryContact == phone);
            if (user is null) throw new InvalidIdException($"Invalid phone");
            var passKeyString = Encoding.UTF8.GetString(user.PasswordKey)
                .RemoveSpecialCharacters();
            return passKeyString != confirmCode ? throw new InvalidIdException($"Invalid code") : user;
        }

        public override IQueryable<Users> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Languages)
                .Include(x => x.Image);
        }

        public async Task<LoginQueryObject> Authenticate(SigninCommand command)
        {
            var param = new DynamicParameters();
            param.Add("phone", command.Phone);
            var user = await _dapperContext.GetAsync<LoginQueryObject>(
                "[dbo].[GetUserForAuthorizationByPhone]", param);

            if (user?.PasswordKey == null || !MatchPasswordHash(command.Password, user.Password, user.PasswordKey))
                return null;
            return user;
        }

        public async Task<LoginQueryObject> GetUserInfoForRefreshToken(string id)
        {
            var param = new DynamicParameters();
            param.Add("id", id);
            var user = await _dapperContext.GetAsync<LoginQueryObject>(
                "[dbo].[GetUserInfoForRefreshToken]", param);
            return user;
        }

        public (byte[], byte[]) Register(UserCommand command)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(command.Password));
            }
            return (passwordHash, passwordKey);
        }
        
        public (byte[], byte[]) Register(string password)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            return (passwordHash, passwordKey);
        }

        private static bool MatchPasswordHash(string passwordText, IReadOnlyList<byte> password, byte[] passwordKey)
        {
            using var hmac = new HMACSHA512(passwordKey);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));
            return !passwordHash.Where((t, i) => t != password[i]).Any();
        }

        protected override Expression<Func<Users, bool>> GetSearchCondition(string search)
        {
            if (int.TryParse(search, out var a))
            {
                return x => x.DisplayName.Contains(search)
                            || x.OtherName.Contains(search)
                            || x.Type == (UserType)a
                            || x.Contact.PrimaryContact.Contains(search);
            }
            return x => x.DisplayName.Contains(search)
                        || x.OtherName.Contains(search)
                        || x.Contact.PrimaryContact.Contains(search);
        }
    }
}