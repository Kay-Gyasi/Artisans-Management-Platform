using System.Threading.Tasks;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Administration;
using AMP.Processors.Repositories.Administration;
using AMP.Processors.Repositories.UoW;

namespace AMP.Persistence.Repositories.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AmpDbContext _dbContext;

        public UnitOfWork(AmpDbContext dbContext, IInitializeDbRepository initializeDbRepository)
        {
            _dbContext = dbContext;
            InitializeDbRepository = initializeDbRepository;
        }

        public IInitializeDbRepository InitializeDbRepository { get; }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}