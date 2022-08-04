using System.Collections.Generic;
using System.Threading.Tasks;
using AMP.Domain.ViewModels;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors
{
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
                LookupType.Artisan => await _uow.Artisans.GetLookupAsync(),
                LookupType.Customer => await _uow.Customers.GetLookupAsync(),
                LookupType.Dispute => await _uow.Disputes.GetLookupAsync(),
                LookupType.Order => await _uow.Orders.GetLookupAsync(),
                LookupType.Payment => await _uow.Payments.GetLookupAsync(),
                LookupType.Rating => await _uow.Ratings.GetLookupAsync(),
                LookupType.Service => await _uow.Services.GetLookupAsync(),
                LookupType.User => await _uow.Users.GetLookupAsync(),
                _ => new List<Lookup>()
            };
        }
    }

    public enum LookupType
    {
        Artisan,
        Customer,
        Dispute, 
        Order,
        Payment,
        Rating,
        Service,
        User
    }
}