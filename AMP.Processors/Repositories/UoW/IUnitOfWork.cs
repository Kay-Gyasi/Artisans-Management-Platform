using System.Threading.Tasks;
using AMP.Processors.Repositories.Administration;

namespace AMP.Processors.Repositories.UoW
{
    public interface IUnitOfWork
    {
        public IInitializeDbRepository InitializeDbRepository { get; }

        Task<bool> SaveChangesAsync();
    }
}