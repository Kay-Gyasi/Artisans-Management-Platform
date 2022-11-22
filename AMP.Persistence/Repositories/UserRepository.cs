using System.Data;
using System.Security.Cryptography;
using System.Text;
using AMP.Processors.Processors.Helpers;
using AMP.Processors.QueryObjects;
using AMP.Processors.Repositories.Base;

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

        public async Task<string> GetIdByPhone(string phone)
        {
            var user = await GetBaseQuery()
                .FirstOrDefaultAsync(x => x.Contact.PrimaryContact == phone);
            return user?.Id;
        }

        public async Task<bool> Exists(string phone)
        {
            return await Task.Run(() => GetBaseQuery()
                .AsNoTracking()
                .Any(x => x.Contact.PrimaryContact == phone));
        }

        public async Task<Users> GetByPhone(string phone) 
            => await GetBaseQuery().FirstOrDefaultAsync(x => x.Contact.PrimaryContact == phone);

        public async Task<Users> GetByPhoneAndConfirmCode(string phone, string confirmCode)
        {
            var user = await GetBaseQuery().FirstOrDefaultAsync(x =>
                x.Contact.PrimaryContact == phone);
            if (user is null) return null;
            var passKeyString = Encoding.UTF8.GetString(user.PasswordKey)
                .RemoveSpecialCharacters();
            return passKeyString != confirmCode ? null : user;
        }

        public override IQueryable<Users> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Languages)
                .Include(x => x.Image);
        }

        public async Task<LoginQueryObject> Authenticate(SigninCommand command)
        {
            var builder = new StringBuilder();
            builder.Append(
                $"select [Password], PasswordKey, Contact_PrimaryContact, DisplayName, Users.Id, FamilyName, ");
            builder.Append($"[Images].ImageUrl, [Type], Contact_EmailAddress, Address_StreetAddress from dbo.[Users] ");
            builder.Append(
                $"LEFT JOIN Images on Users.ImageId = Images.Id where Contact_PrimaryContact = {command.Phone} ");
            builder.Append("and IsVerified = 1");
            var user = await Task.FromResult(_dapperContext.Get<LoginQueryObject>(
                builder.ToString().AddToWhereClause(), null,
                CommandType.Text));

            if (user?.PasswordKey == null || !MatchPasswordHash(command.Password, user.Password, user.PasswordKey))
                return null;
            return user;
        }

        public async Task<LoginQueryObject> GetUserInfoForRefreshToken(string id)
        {
            var builder = new StringBuilder();
            builder.Append(
                $"select [Password], PasswordKey, Contact_PrimaryContact, DisplayName, Users.Id, FamilyName, ");
            builder.Append($"[Images].ImageUrl, [Type], Contact_EmailAddress, Address_StreetAddress from dbo.[Users] ");
            builder.Append(
                $"LEFT JOIN Images on Users.ImageId = Images.Id where Users.Id = {id} ");
            builder.Append("and IsVerified = 1");
            var user = await Task.FromResult(_dapperContext.Get<LoginQueryObject>(
                builder.ToString().AddToWhereClause(), null,
                CommandType.Text));
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

        private static bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
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