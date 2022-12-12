using AMP.Domain.ViewModels;

namespace Amp.IntegrationTests.Tests;

public class ArtisanControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private static readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/artisans";
    public ArtisanControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #region GetPage

    [Fact]
    public async Task GetPage_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var command = new PaginatedCommand
        {
            PageSize = 5
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }

    [Fact]
    public async Task GetPage_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var command = new PaginatedCommand
        {
            PageSize = 5
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetPage_ByCustomer_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<ArtisanPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().BeGreaterOrEqualTo(0);
        data?.Data.Count.Should().BeLessOrEqualTo(pageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }
    
    [Fact]
    public async Task GetPage_WithSearchTerm_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize,
            Search = (await _factory.UnitOfWork.Services.GetAsync("6079bd37-b82d-4f3b-864d-2ee0ed7fd404"))?
                .Name
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<ArtisanPageDto>>();
        timer.Stop();
    
        // Assert
        var service = await _factory.UnitOfWork.Services
            .GetBaseQuery()
            .Include(x => x.Artisans)
            .FirstOrDefaultAsync(x => x.Name == command.Search);
        foreach (var offersService in data?.Data?
                     .Select(artisan => service?.Artisans.Any(x => x.Id == artisan.Id)))
        {
            offersService.Should().BeTrue();
        }
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion
    
    #region Get

    [Fact]
    public async Task Get_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var artisanId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{artisanId}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var artisanId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{artisanId}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Theory]
    [MemberData(nameof(GetUserTypes))]
    public async Task Get_ValidId_ReturnsOK(UserType type)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, type);
        var timer = new Stopwatch();
        var artisanId = (await _factory.UnitOfWork.Artisans.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{artisanId}");
        var artisan = await request.Content.ReadFromJsonAsync<ArtisanDto>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        artisan.Should().NotBeNull();
        artisan?.Id.Should().Be(artisanId);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    #endregion

    #region GetByUser

    [Fact]
    public async Task GetByUser_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetByUser");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    [Fact]
    public async Task GetByUser_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var userId = (await _factory.UnitOfWork.Users
            .GetBaseQuery()
            .FirstOrDefaultAsync(x => x.Contact.PrimaryContact == "0557833216"))?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetByUser");
        var artisan = await request.Content.ReadFromJsonAsync<ArtisanDto>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        artisan.Should().NotBeNull();
        artisan?.UserId.Should().Be(userId);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    #endregion

    #region GetArtisansWorkedForCustomer

    [Fact]
    public async Task GetArtisansWorkedForCustomer_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetArtisansWorkedForCustomer");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    [Fact]
    public async Task GetArtisansWorkedForCustomer_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetArtisansWorkedForCustomer");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task GetArtisansWorkedForCustomer_ByCustomer_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetArtisansWorkedForCustomer");
        var lookup = await request.Content.ReadFromJsonAsync<List<Lookup>>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        lookup?.Should().NotBeNull();
        lookup?.Count.Should().Be(1);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion
    
    #region Delete

    [Fact]
    public async Task Delete_AnonymousUser_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Fact]
    public async Task Delete_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Fact]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Artisans.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        var artisan = await _factory.UnitOfWork.Artisans.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        artisan.Should().BeNull();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    #region Save

    [Fact]
    public async Task Save_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var order = _fixture.Build<ArtisanCommand>()
            .Without(x => x.Services)
            .Without(x => x.Eccn)
            .Without(x => x.IsVerified)
            .Without(x => x.IsApproved)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Fact]
    public async Task Save_ByArtisan_ReturnsCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var artisan = _fixture.Build<ArtisanCommand>()
            .Without(x => x.Id)
            .Without(x => x.Services)
            .Without(x => x.Eccn)
            .Without(x => x.IsVerified)
            .Without(x => x.IsApproved)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", artisan);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Created);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Update_ByArtisan_ReturnsCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Artisans.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var artisan = _fixture.Build<ArtisanCommand>()
            .With(x => x.Id, id)
            .Without(x => x.Services)
            .Without(x => x.Eccn)
            .Without(x => x.IsVerified)
            .Without(x => x.IsApproved)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", artisan);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Created);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    [Fact]
    public async Task Save_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var order = _fixture.Build<ArtisanCommand>()
            .Without(x => x.Services)
            .Without(x => x.Eccn)
            .Without(x => x.IsVerified)
            .Without(x => x.IsApproved)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    // [Theory]
    // [MemberData(nameof(GetValidationTextObjs))]
    // public async Task Save_WithoutRequiredInfo_ReturnsPreconditionFailed(OrderCommand order)
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();
    //     await _factory.AuthenticateAsync(client, UserType.Customer);
    //     var timer = new Stopwatch();
    //
    //     // Act
    //     timer.Start();
    //     var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
    //     timer.Start();
    //
    //     // Assert
    //     request.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
    //     timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    // }  

    #endregion

    
    
    
    public static IEnumerable<object[]> GetUserTypes()
    {
        yield return new object[]{UserType.Artisan};
        yield return new object[]{UserType.Customer};
    }
    
    public static IEnumerable<object[]> GetValidationTextObjs()
    {
        yield return new object[]
        {
            _fixture.Build<ArtisanCommand>()
                .Without(x => x.UserId)
                .Create()
        };
        yield return new object[]
        {
            _fixture.Build<ArtisanCommand>()
                .Without(x => x.BusinessName)
                .Create()
        };
    }
}