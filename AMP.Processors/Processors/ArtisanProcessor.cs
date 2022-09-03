using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AMP.Domain.Entities;
using AMP.Domain.ViewModels;
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
    public class ArtisanProcessor : ProcessorBase
    {
        private const string LookupCacheKey = "Artisanlookup";

        public ArtisanProcessor(IUnitOfWork uow, 
            IMapper mapper, 
            IMemoryCache cache) : base(uow, mapper, cache)
        {
        }

        public async Task<int> Save(ArtisanCommand command)
        {
            var isNew = command.Id == 0;
            Artisans artisan;

            if (isNew)
            {
                artisan = Artisans.Create(command.UserId)
                    .CreatedOn(DateTime.UtcNow);
                await AssignFields(artisan, command, true);
                _cache.Remove(LookupCacheKey);
                await _uow.Artisans.InsertAsync(artisan);
                await _uow.SaveChangesAsync();
                return artisan.Id;
            }

            artisan = await _uow.Artisans.GetAsync(command.Id);
            await AssignFields(artisan, command);
            _cache.Remove(LookupCacheKey);
            await _uow.Artisans.UpdateAsync(artisan);
            await _uow.SaveChangesAsync();
            return artisan.Id;
        }

        public async Task<PaginatedList<ArtisanPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await _uow.Artisans.GetPage(command, new CancellationToken());
            var paginated = _mapper.Map<PaginatedList<ArtisanPageDto>>(page);
            var data = paginated.Data.Select(x => new ArtisanPageDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                BusinessName = x.BusinessName,
                Description = x.Description,
                IsVerified = x.IsVerified,
                IsApproved = x.IsApproved,
                Rating = _uow.Ratings.GetRating(x.Id),
                User = x.User
            }).ToList();
            return new PaginatedList<ArtisanPageDto>(data, paginated.TotalCount, paginated.CurrentPage,
                paginated.PageSize);
        }

        public async Task<ArtisanDto> Get(int id)
        {
            var artisan = _mapper.Map<ArtisanDto>(await _uow.Artisans.GetAsync(id));
            artisan.NoOfOrders = _uow.Orders.GetCount(artisan.Id);
            artisan.NoOfReviews = _uow.Ratings.GetCount(artisan.Id);
            artisan.Rating = _uow.Ratings.GetRating(artisan.Id);
            return artisan;
        }

        public async Task<ArtisanDto> GetByUserId(int userId)
        {
            var artisan = _mapper.Map<ArtisanDto>(await _uow.Artisans.GetArtisanByUserId(userId));
            artisan.NoOfOrders = _uow.Orders.GetCount(artisan.Id);
            artisan.NoOfReviews = _uow.Ratings.GetCount(artisan.Id);
            artisan.Rating = _uow.Ratings.GetRating(artisan.Id);
            return artisan;
        }

        public async Task<List<Lookup>> GetArtisansWhoHaveWorkedForCustomer(int userId)
        {
            return await Task.Run(() => _uow.Artisans.GetArtisansWhoHaveWorkedForCustomer(userId));
        }

        public async Task Delete(int id)
        {
            var artisan = await _uow.Artisans.GetAsync(id);
            _cache.Remove(LookupCacheKey);
            if (artisan != null) await _uow.Artisans.SoftDeleteAsync(artisan);
            await _uow.SaveChangesAsync();
        }

        private async Task AssignFields(Artisans artisan, ArtisanCommand command, bool isNew = false)
        {
            var names = command.Services.Select(service => service.Name).ToList();

            var services = await _uow.Services.BuildServices(names);
            artisan.WithBusinessName(command.BusinessName)
                .WithDescription(command.Description)
                .IsVerifiedd(command.IsVerified)
                .IsApprovedd(command.IsApproved)
                .Offers(services);
            if (!isNew) artisan.ForUserId(command.UserId);
        }
    }
}