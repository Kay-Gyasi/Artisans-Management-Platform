using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Commands;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class UserRepository : RepositoryBase<Users>, IUserRepository
    {
        public UserRepository(AmpDbContext context, ILogger<Users> logger) : base(context, logger)
        {
        }

        public async Task<string> GetIdByPhone(string phone)
        {
            var user = await GetBaseQuery()
                .FirstOrDefaultAsync(x => x.Contact.PrimaryContact == phone);
            return user.Id;
        }

        public async Task<bool> Exists(string phone)
        {
            return GetBaseQuery()
                .AsNoTracking()
                .Any(x => x.Contact.PrimaryContact == phone);
        }

        public override IQueryable<Users> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.Languages)
                .Include(x => x.Image);
        }

        public async Task<Users> Authenticate(SigninCommand command)
        {
            var user = await GetBaseQuery()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Contact.PrimaryContact == command.Phone);
            if (user?.PasswordKey == null || !MatchPasswordHash(command.Password, user.Password, user.PasswordKey))
                return null;
            

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
    }
}