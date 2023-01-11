﻿using AMP.Domain.Entities.BusinessManagement;
using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;

namespace AMP.Processors.Processors.BusinessManagement
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

            Rating rating;
            if (isNew)
            {
                var customer = await Uow.Customers.GetCustomerId(command.UserId);
                await Uow.Ratings.DeletePreviousRatingForSameArtisan(customer, command.ArtisanId);
                rating = Rating.Create(customer, command.ArtisanId);
                await AssignFields(rating, command, true);
                Cache.Remove(LookupCacheKey);
                await Uow.Ratings.InsertAsync(rating);
                await Uow.SaveChangesAsync();
                return rating.Id;
            }

            rating = await Uow.Ratings.GetAsync(command.Id);
            await AssignFields(rating, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Ratings.UpdateAsync(rating);
            await Uow.SaveChangesAsync();
            return rating.Id;
        }

        public async Task<PaginatedList<RatingPageDto>> GetArtisanRatingPage(PaginatedCommand command, string userId)
        {
            var page = await Uow.Ratings.GetArtisanRatingPage(command, userId, new CancellationToken());
            foreach(var rating in page.Data)
            {
                rating.ForArtisan(await Uow.Artisans.GetAsync(rating.ArtisanId))
                    .ForCustomer(await Uow.Customers.GetAsync(rating.CustomerId));
            }
            return Mapper.Map<PaginatedList<RatingPageDto>>(page);
        }
        public async Task<PaginatedList<RatingPageDto>> GetCustomerRatingPage(PaginatedCommand command, string userId)
        {
            var page = await Uow.Ratings.GetCustomerRatingPage(command, userId, new CancellationToken());
            foreach(var rating in page.Data)
            {
                rating.ForArtisan(await Uow.Artisans.GetAsync(rating.ArtisanId))
                    .ForCustomer(await Uow.Customers.GetAsync(rating.CustomerId));
            }
            return Mapper.Map<PaginatedList<RatingPageDto>>(page);
        }

        public async Task<RatingDto> Get(string id)
        {
            return Mapper.Map<RatingDto>(await Uow.Ratings.GetAsync(id));
        }

        public async Task Delete(string id)
        {
            var rating = await Uow.Ratings.GetAsync(id);
            Cache.Remove(LookupCacheKey);
            if (rating != null) await Uow.Ratings.SoftDeleteAsync(rating);
            await Uow.SaveChangesAsync();
        }

        private async Task AssignFields(Rating rating, RatingCommand command, bool isNew = false)
        {
            rating.WithVotes(command.Votes)
                .WithDescription(command.Description);

            if (!isNew)
            {
                var customer = await Uow.Customers.GetCustomerId(command.UserId);
                rating.ForCustomerWithId(customer)
                    .ForArtisanWithId(command.ArtisanId);
            }
        }
    }
}