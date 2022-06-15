using System.Threading.Tasks;

namespace AMP.Processors.Repositories.Administration
{
    public interface IInitializeDbRepository
    {
        Task InitializeDatabase();
    }
}
