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

        public async Task<string> Save(ArtisanCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);
            Artisans artisan;

            if (isNew)
            {
                artisan = Artisans.Create(command.UserId)
                    .CreatedOn();
                await AssignFields(artisan, command, true);
                Cache.Remove(LookupCacheKey);
                await Uow.Artisans.InsertAsync(artisan);
                await Uow.SaveChangesAsync();
                return artisan.Id;
            }

            artisan = await Uow.Artisans.GetAsync(command.Id);
            await AssignFields(artisan, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Artisans.UpdateAsync(artisan);
            await Uow.SaveChangesAsync();
            return artisan.Id;
        }

        public async Task<PaginatedList<ArtisanPageDto>> GetPage(PaginatedCommand command)
        {
            var page = await Uow.Artisans.GetArtisanPage(command, new CancellationToken());
            var paginated = Mapper.Map<PaginatedList<ArtisanPageDto>>(page);
            var data = paginated.Data.Select(x => new ArtisanPageDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                BusinessName = x.BusinessName,
                Description = x.Description,
                IsVerified = x.IsVerified,
                IsApproved = x.IsApproved,
                Rating = Uow.Ratings.GetRating(x.Id),
                User = x.User,
                ImageUrl = x.User?.Image?.ImageUrl
            }).ToList();
            return new PaginatedList<ArtisanPageDto>(data, paginated.TotalCount, paginated.CurrentPage,
                paginated.PageSize);
        }

        public async Task<ArtisanDto> Get(string id)
        {
            var artisan = Mapper.Map<ArtisanDto>(await Uow.Artisans.GetAsync(id));
            artisan.NoOfOrders = await Uow.Orders.GetCount(artisan.Id);
            artisan.NoOfReviews = Uow.Ratings.GetCount(artisan.Id);
            artisan.Rating = Uow.Ratings.GetRating(artisan.Id);
            return artisan;
        }

        public async Task<ArtisanDto> GetByUserId(string userId)
        {
            var artisan = Mapper.Map<ArtisanDto>(await Uow.Artisans.GetArtisanByUserId(userId));
            artisan.NoOfOrders = await Uow.Orders.GetCount(artisan.Id);
            artisan.NoOfReviews = Uow.Ratings.GetCount(artisan.Id);
            artisan.Rating = Uow.Ratings.GetRating(artisan.Id);
            return artisan;
        }

        public async Task<List<Lookup>> GetArtisansWhoHaveWorkedForCustomer(string userId)
        {
            return await Task.Run(() => Uow.Artisans.GetArtisansWhoHaveWorkedForCustomer(userId));
        }

        public async Task Delete(string id)
        {
            var artisan = await Uow.Artisans.GetAsync(id);
            artisan?.SetLastModified();
            Cache.Remove(LookupCacheKey);
            if (artisan != null) await Uow.Artisans.SoftDeleteAsync(artisan);
            await Uow.SaveChangesAsync();
        }

        private async Task AssignFields(Artisans artisan, ArtisanCommand command, bool isNew = false)
        {
            var names = command.Services.Select(service => service.Name).ToList();

            var services = await Uow.Services.BuildServices(names);
            artisan.WithBusinessName(command.BusinessName)
                .WithDescription(command.Description)
                .IsVerifiedd(command.IsVerified)
                .IsApprovedd(command.IsApproved)
                .Offers(services)
                .OfType(command.Type)
                .HasEccn(command.Eccn);
            if (!isNew) artisan.ForUserId(command.UserId)
                .SetLastModified();
        }
    }
}