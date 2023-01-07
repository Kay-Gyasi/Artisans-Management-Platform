using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement
{
    public interface ILanguageRepository : IRepositoryBase<Language>
    {
        Task<List<Language>> BuildLanguages(IEnumerable<string> languages);
    }
}