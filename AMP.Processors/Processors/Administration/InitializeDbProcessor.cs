using System.Threading.Tasks;
using AMP.Processors.Repositories.Administration;
using AMP.Processors.Repositories.UoW;

namespace AMP.Processors.Processors.Administration
{
    [Processor]
    public class InitializeDbProcessor
    {
        private readonly IUnitOfWork _uow;

        public InitializeDbProcessor(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task InitializeDatabase()
        {
            await _uow.InitializeDbRepository.InitializeDatabase();
            await _uow.SaveChangesAsync();
        }
    }
}