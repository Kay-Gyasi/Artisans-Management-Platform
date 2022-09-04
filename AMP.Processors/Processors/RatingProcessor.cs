using System;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Processors.Commands;
using AMP.Processors.Dtos;
using AMP.Processors.PageDtos;
using AMP.Processors.Processors.Base;
using AMP.Processors.Repositories.UoW;
using AMP.Shared.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;

namespace AMP.Processors.Processors
{
    [Processor]
    public class RatingProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Ratinglookup";

        public RatingProcessor(IUnitOfWork uow, IMapper mapper, IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<string> Save(RatingCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);

            Ratings rating;
            if (isNew)
            {
                var customer = await _uow.Customers.GetCustomerId(command.UserId);
                await _uow.Ratings.OverridePreviousRating(customer, command.ArtisanId);
                rating = Ratings.Create(customer, command.ArtisanId)
                    .CreatedOn(DateTime.UtcNow);
                await AssignFields(rating, command, true);
                _cache.Remove(LookupCacheKey);
                await _uow.Ratings.InsertAsync(rating);
                await _uow.SaveChangesAsync();
                return rating.Id;
            }

            rating = await _uow.Ratings.GetAsync(command.Id);
            await AssignFields(rating, command);
            _cache.Remove(LookupCacheKey);
            await _uow.Ratings.UpdateAsync(rating);
            await _uow.SaveChangesAsync();
            return rating.Id;
        }

        public async Task<PaginatedList<RatingPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Ratings.GetPage(command, new CancellationToken());
            return _mapper.Map<PaginatedList<RatingPageDto>>(page);
        }
        
        public async Task<PaginatedList<RatingPageDto>> GetArtisanRatingPage(PaginatedCommand command, string userId)
        {
            var page = await _uow.Ratings.GetArtisanRatingPage(command, userId, new CancellationToken());
            foreach(var rating in page.Data)
            {
                rating.ForArtisan(await _uow.Artisans.GetAsync(rating.ArtisanId))
                    .ForCustomer(await _uow.Customers.GetAsync(rating.CustomerId));
            }
            return _mapper.Map<PaginatedList<RatingPageDto>>(page);
        }

        public async Task<RatingDto> Get(string id)
        {
            return _mapper.Map<RatingDto>(await _uow.Ratings.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var rating = await _uow.Ratings.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (rating != null) await _uow.Ratings.SoftDeleteAsync(rating);
            await _uow.SaveChangesAsync();
        }

        private async Task AssignFields(Ratings rating, RatingCommand command, bool isNew = false)
        {
            rating.WithVotes(command.Votes)
                .WithDescription(command.Description);

            if (!isNew)
            {
                var customer = await _uow.Customers.GetCustomerId(command.UserId);
                rating.ForCustomerWithId(customer)
                        .ForArtisanWithId(command.ArtisanId);
            }
        }
    }
}