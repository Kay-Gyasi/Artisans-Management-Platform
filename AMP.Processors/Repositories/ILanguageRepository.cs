﻿using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface ILanguageRepository : IRepositoryBase<Languages>
    {
        Task<List<Languages>> BuildLanguages(IEnumerable<string> languages);
    }
}