using System.Collections.Generic;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Interfaces.Base;

namespace AMP.Processors.Repositories
{
    public interface ILanguageRepository : IRepositoryBase<Languages>
    {
        Task<List<Languages>> BuildLanguages(List<string> languages);
    }
}