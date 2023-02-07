using AMP.Domain.Entities.UserManagement;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Dtos.UserManagement;
using AMP.Processors.PageDtos.UserManagement;
using Microsoft.AspNetCore.Http;

namespace AMP.Processors.Processors.UserManagement
{
    [Processor]
    public class ArtisanProcessor : Processor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string LookupCacheKey = "Artisanlookup";

        public ArtisanProcessor(IUnitOfWork uow, 
            IMapper mapper, 
            IMemoryCache cache,
            IHttpContextAccessor httpContextAccessor) : base(uow, mapper, cache)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<string>> Save(ArtisanCommand command)
        {
            var isNew = string.IsNullOrEmpty(command.Id);
            command.UserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Artisan artisan;

            if (isNew)
            {
                artisan = Artisan.Create(command.UserId)
                    .CreatedOn();
                await AssignFields(artisan, command, true);
                Cache.Remove(LookupCacheKey);
                await Uow.Artisans.InsertAsync(artisan);
                await Uow.SaveChangesAsync();
                return new Result<string>(artisan.Id);
            }

            artisan = await Uow.Artisans.GetAsync(command.Id);
            if (artisan is null)
                return new Result<string>(
                    new InvalidIdException($"Artisan with id: {artisan.Id} does not exist"));
            await AssignFields(artisan, command);
            Cache.Remove(LookupCacheKey);
            await Uow.Artisans.UpdateAsync(artisan);
            await Uow.SaveChangesAsync();
            return new Result<string>(artisan.Id);
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

        public async Task<Result<ArtisanDto>> Get(string id)
        {
            var artisan = Mapper.Map<ArtisanDto>(await Uow.Artisans.GetAsync(id));
            if (artisan is null)
                return new Result<ArtisanDto>(
                    new InvalidIdException($"Artisan with id: {id} does not exist"));
            artisan.NoOfOrders = await Uow.Orders.GetCount(artisan.Id);
            artisan.NoOfReviews = Uow.Ratings.GetCount(artisan.Id);
            artisan.Rating = Uow.Ratings.GetRating(artisan.Id);
            return new Result<ArtisanDto>(artisan);
        }

        public async Task<Result<ArtisanDto>> GetByUserId(string userId)
        {
            var artisan = Mapper.Map<ArtisanDto>(await Uow.Artisans.GetArtisanByUserId(userId));
            if (artisan is null)
                return new Result<ArtisanDto>(
                    new InvalidIdException($"Artisan with userId: {userId} does not exist"));
            artisan.NoOfOrders = await Uow.Orders.GetCount(artisan.Id);
            artisan.NoOfReviews = Uow.Ratings.GetCount(artisan.Id);
            artisan.Rating = Uow.Ratings.GetRating(artisan.Id);
            return new Result<ArtisanDto>(artisan);
        }

        public async Task<Result<List<Lookup>>> GetArtisansWhoHaveWorkedForCustomer(string userId)
        {
            try
            {
                return new Result<List<Lookup>>(await Uow.Artisans.GetArtisansWhoHaveWorkedForCustomer(userId));
            }
            catch (Exception e)
            {
                return new Result<List<Lookup>>(e);
            }
        }

        public async Task<Result<bool>> Delete(string id)
        {
            try
            {
                await Uow.Artisans.SoftDeleteAsync(id);
                Cache.Remove(LookupCacheKey);
                return new Result<bool>(true);
            }
            catch (Exception e)
            {
                return new Result<bool>(e);
            }
        }

        private async Task AssignFields(Artisan artisan, ArtisanCommand command, bool isNew = false)
        {
            artisan.WithBusinessName(command.BusinessName ?? "")
                .WithDescription(command.Description ?? "")
                .IsVerifiedd(command.IsVerified)
                .IsApprovedd(command.IsApproved)
                .OfType(command.Type)
                .HasEccn(command.Eccn);
            if (!isNew) artisan.ForUserId(command.UserId);
            if (command.Services is not null)
            {
                var names = command.Services?.Select(service => service.Name);
                var services = await Uow.Services.BuildServices(names);
                artisan.Offers(services);
            }
        }
    }
}