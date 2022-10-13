using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.Enums;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories;

[Repository]
public class RegistrationRepository : RepositoryBase<Registrations>, IRegistrationRepository
{
    public RegistrationRepository(AmpDbContext context, ILogger<Registrations> logger) : base(context, logger)
    {
    }

    public async Task<bool> Crosscheck(string phone, string code) 
        => await GetBaseQuery().AnyAsync(x => x.VerificationCode == code && x.Phone == phone);

    public async Task<Registrations> GetByPhone(string phone) 
        => await GetBaseQuery().FirstOrDefaultAsync(x => x.Phone == phone);

    public async Task Verify(string phone, string code)
    {
        var registration = await GetBaseQuery().FirstOrDefaultAsync(x => x.VerificationCode == code);
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Contact.PrimaryContact == phone);
        user?.Verify();

        await DeleteAsync(registration, new CancellationToken());
        await Context.SaveChangesAsync();
    }
}