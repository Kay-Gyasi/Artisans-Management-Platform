﻿using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Interfaces.Base;
using AMP.Shared.Domain.Models;

namespace AMP.Processors.Repositories
{
    public interface IRatingRepository : IRepositoryBase<Ratings>
    {
        double GetRating(string artisanId);
        int GetCount(string artisanId);
        Task OverridePreviousRating(string customerId, string artisanId);

        Task<PaginatedList<Ratings>> GetArtisanRatingPage(PaginatedCommand paginated, string userId,
            CancellationToken cancellationToken);
    }
}