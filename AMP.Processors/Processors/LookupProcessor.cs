using AMP.Domain.ViewModels;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AMP.Processors.Processors
{
    public enum LookupType
    {
        Artisan,
        Customer,
        Dispute,
        Order,
        Payment,
        Rating,
        Service,
        User,
        ArtisanServices
    }

    [Processor]
    public class LookupProcessor : ProcessorBase
    {
        public LookupProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<List<Lookup>> GetLookup(LookupType type)
        {
            return type switch
            {
                LookupType.Artisan => await Get(LookupType.Artisan, await _uow.Artisans.GetLookupAsync()),
                LookupType.Customer => await Get(LookupType.Customer, await _uow.Customers.GetLookupAsync()),
                LookupType.Dispute => await Get(LookupType.Dispute, await _uow.Disputes.GetLookupAsync()),
                LookupType.Order => await Get(LookupType.Order, await _uow.Orders.GetLookupAsync()),
                LookupType.Payment => await Get(LookupType.Payment, await _uow.Payments.GetLookupAsync()),
                LookupType.Rating => await Get(LookupType.Rating, await _uow.Ratings.GetLookupAsync()),
                LookupType.Service => await Get(LookupType.Service, await _uow.Services.GetLookupAsync()),
                LookupType.User => await Get(LookupType.User, await _uow.Users.GetLookupAsync()),
                LookupType.ArtisanServices => await Get(LookupType.ArtisanServices, await _uow.Services.GetAvailableServices()),
                _ => new List<Lookup>()
            };
        }

        public async Task<List<Lookup>> GetOpenOrdersLookup(string userId)
        {
            return await _uow.Orders.GetOpenOrdersLookup(userId);
        }

        private async Task<List<Lookup>> Get(LookupType type, List<Lookup> lookup)
        {
            var cacheKey = string.Join("", new[] { type.ToString(), "lookup" });
            var cache = _cache.Get<List<Lookup>>(cacheKey);
            if (cache != null) return cache;
            _cache.Set(cacheKey, lookup);
            return lookup;
        }
    }
}