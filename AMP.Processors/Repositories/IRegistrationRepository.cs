using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories;

public interface IRegistrationRepository : IRepositoryBase<Registrations>
{
    Task Verify(string phone, string code);
    Task<bool> Crosscheck(string phone, string code);
    Task<Registrations> GetByPhone(string phone);
}