﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.ViewModels;
using AMP.Persistence.Database;
using AMP.Persistence.Repositories.Base;
using AMP.Processors.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AMP.Persistence.Repositories
{
    [Repository]
    public class ArtisanRepository : RepositoryBase<Artisans>, IArtisanRepository
    {
        public ArtisanRepository(AmpDbContext context, ILogger<Artisans> logger) : base(context, logger)
        {
        }

        public List<Lookup> GetArtisansWhoHaveWorkedForCustomer(string userId)
        {
            var artisans = GetBaseQuery().Where(x => x.Orders.Any(a => a.Customer.UserId == userId));
            return artisans.Select(x => new Lookup
            {
                Id = x.Id,
                Name = x.BusinessName
            }).ToList();
        }

        public async Task<Artisans> GetArtisanByUserId(string userId)
        {
            return await GetBaseQuery().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        protected override Expression<Func<Artisans, bool>> GetSearchCondition(string search)
        {
            return x => x.Services.Any(a => a.Name == search);
        }


        public override Task<List<Lookup>> GetLookupAsync()
        {
            return GetBaseQuery().Select(x => new Lookup()
            {
                Id = x.Id,
                Name = x.BusinessName
            }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public override IQueryable<Artisans> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(x => x.User)
                .ThenInclude(x => x.Languages)
                .Include(x => x.Services)
                .Include(x => x.Ratings)
                .Include(x => x.User)
                .ThenInclude(x => x.Image); ;
        }
    }
}