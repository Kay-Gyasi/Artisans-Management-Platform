using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.UserManagement;

namespace AMP.Persistence.Repositories.UserManagement;

[Repository]
public class RegistrationRepository : RepositoryBase<Registration>, IRegistrationRepository
{
    public RegistrationRepository(AmpDbContext context, ILogger<Registration> logger) : base(context, logger)
    {
    }

    public Task<bool> Crosscheck(string phone, string code) 
        => GetBaseQuery().AnyAsync(x => x.VerificationCode == code && x.Phone == phone);

    public Task<Registration> GetByPhone(string phone) 
        => GetBaseQuery().FirstOrDefaultAsync(x => x.Phone == phone);

    public async Task Verify(string phone, string code)
    {
        var registration = await GetBaseQuery().FirstOrDefaultAsync(x => x.VerificationCode == code);
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Contact.PrimaryContact == phone);
        user?.Verify();

        await DeleteAsync(registration, new CancellationToken());
        await Context.SaveChangesAsync();
    }
}