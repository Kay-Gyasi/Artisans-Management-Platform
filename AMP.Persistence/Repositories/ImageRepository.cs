using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class ImageRepository : RepositoryBase<Images>, IImageRepository
    {
        public ImageRepository(AmpDbContext context, ILogger<Images> logger) : base(context, logger)
        {
        }
    }
}