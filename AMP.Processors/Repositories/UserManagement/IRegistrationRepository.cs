using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement;

public interface IRegistrationRepository : IRepositoryBase<Registration>
{
    Task Verify(string phone, string code);
    Task<bool> Crosscheck(string phone, string code);
    Task<Registration> GetByPhone(string phone);
}