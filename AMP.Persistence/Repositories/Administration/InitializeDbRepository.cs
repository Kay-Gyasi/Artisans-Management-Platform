using System.Threading.Tasks;
using AMP.Processors.Repositories.Administration;

namespace AMP.Persistence.Repositories.Administration
{
    [Repository]
    public class InitializeDbRepository : IInitializeDbRepository
    {
        public Task InitializeDatabase()
        {
            throw new System.NotImplementedException();
        }
    }
}