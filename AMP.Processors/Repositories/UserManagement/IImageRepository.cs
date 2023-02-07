using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Repositories.Base;

namespace AMP.Processors.Repositories.UserManagement
{
    public interface IImageRepository : IRepository<Image>
    {
        Task RemoveCurrentDetails(string userId);
    }
}