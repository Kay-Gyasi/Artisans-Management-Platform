using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories
{
    public interface IImageRepository : IRepositoryBase<Images>
    {
        Task RemoveCurrentDetails(string userId);
    }
}