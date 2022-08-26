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

        public async Task<int> Save(RatingCommand command)
        {
            var isNew = command.Id == 0;

            Ratings rating;
            if (isNew)
            {
                rating = Ratings.Create(command.CustomerId, command.ArtisanId)
                    .CreatedOn(DateTime.UtcNow);
                AssignFields(rating, command, true);
                _cache.Remove(LookupCacheKey);
                await _uow.Ratings.InsertAsync(rating);
                await _uow.SaveChangesAsync();
                return rating.Id;
            }

            rating = await _uow.Ratings.GetAsync(command.Id);
            AssignFields(rating, command);
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

        public async Task<RatingDto> Get(int id)
        {
            return _mapper.Map<RatingDto>(await _uow.Ratings.GetAsync(id));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Ratings.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (artisan != null) await _uow.Ratings.DeleteAsync(artisan, new CancellationToken());
            await _uow.SaveChangesAsync();
        }

        private static void AssignFields(Ratings rating, RatingCommand command, bool isNew = false)
        {
            rating.WithVotes(command.Votes)
                .WithDescription(command.Description);

            if (!isNew)
                rating.ForCustomerWithId(command.CustomerId)
                    .ForArtisanWithId(command.ArtisanId);
        }
    }
}