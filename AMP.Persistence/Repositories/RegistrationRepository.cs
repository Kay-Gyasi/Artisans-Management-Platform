namespace AMP.Persistence.Repositories;

[Repository]
public class RegistrationRepository : RepositoryBase<Registrations>, IRegistrationRepository
{
    public RegistrationRepository(AmpDbContext context, ILogger<Registrations> logger) : base(context, logger)
    {
    }

    public Task<bool> Crosscheck(string phone, string code) 
        => GetBaseQuery().AnyAsync(x => x.VerificationCode == code && x.Phone == phone);

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