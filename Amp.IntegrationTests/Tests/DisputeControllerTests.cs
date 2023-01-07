using AMP.Processors.Commands.BusinessManagement;
using AMP.Processors.Dtos.BusinessManagement;
using AMP.Processors.PageDtos.BusinessManagement;

namespace Amp.IntegrationTests.Tests;

public class DisputeControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private static readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/disputes";
    public DisputeControllerTests(CustomWebApplicationFactory<Program> factory)
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
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<DisputePageDto>>();
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
            Search = "failed"
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<DisputePageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().Be(1);
        data?.TotalCount.Should().Be(1);
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
        var disputeId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{disputeId}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Get_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var disputeId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{disputeId}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var disputeId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{disputeId}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Get_ValidId_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var disputeId = (await _factory.UnitOfWork.Disputes.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{disputeId}");
        var dispute = await request.Content.ReadFromJsonAsync<DisputeDto>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        dispute.Should().NotBeNull();
        dispute?.Id.Should().Be(disputeId);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    #endregion

    #region GetOpenDisputeCount

    [Fact]
    public async Task GetOpenDisputeCount_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var disputeId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetOpenDisputeCount");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    [Fact]
    public async Task GetOpenDisputeCount_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetOpenDisputeCount");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    [Fact]
    public async Task GetOpenDisputeCount_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var orderId = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var dispute = _fixture.Build<DisputeCommand>()
            .Without(x => x.Id)
            .With(x => x.OrderId, orderId)
            .Create();
        var setup = await client.PostAsJsonAsync($"{BaseUrl}/Save", dispute);

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetOpenDisputeCount");
        var results = await request.Content.ReadFromJsonAsync<DisputeCount>();
        timer.Stop();

        // Assert
        setup.StatusCode.Should().Be(HttpStatusCode.Created);
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        results.Should().NotBeNull();
        results?.Count.Should().BeGreaterThan(0);
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
    public async Task Delete_ByArtisan_ReturnsForbidden()
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
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Fact]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
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
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Disputes.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        var dispute = await _factory.UnitOfWork.Disputes.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        dispute.Should().BeNull();
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
        var order = _fixture.Build<DisputeCommand>()
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
    public async Task Save_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var order = _fixture.Build<DisputeCommand>()
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Theory]
    [MemberData(nameof(GetValidationTextObjs))]
    public async Task Save_WithoutRequiredInfo_ReturnsPreconditionFailed(DisputeCommand dispute)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", dispute);
        timer.Start();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }

    [Fact]
    public async Task Save_NewDispute_ReturnsCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var orderId = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var dispute = _fixture.Build<DisputeCommand>()
            .Without(x => x.Id)
            .With(x => x.OrderId, orderId)
            .Create();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", dispute);
        _factory.UnitOfWork.Disputes.GetDbContext().ChangeTracker.Clear();
        var inserted = await _factory.UnitOfWork.Disputes
            .GetBaseQuery()
            .FirstOrDefaultAsync(x => x.Details == dispute.Details);
        timer.Start();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Created);
        inserted.Should().NotBeNull();
        inserted?.DateCreated.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
        inserted?.DateModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Save_UpdateDispute_ReturnsCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var orderId = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var disputeId = (await _factory.UnitOfWork.Disputes.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var dispute = _fixture.Build<DisputeCommand>()
            .With(x => x.Id, disputeId)
            .With(x => x.OrderId, orderId)
            .Create();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", dispute);
        _factory.UnitOfWork.Disputes.GetDbContext().ChangeTracker.Clear();
        var inserted = await _factory.UnitOfWork.Disputes.GetAsync(disputeId);
        timer.Start();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Created);
        inserted.Should().NotBeNull();
        inserted?.Details.Should().Be(dispute.Details);
        inserted?.OrderId.Should().Be(dispute.OrderId);
        inserted?.DateModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMinutes(1));
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Save_UpdateDisputeWithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var orderId = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var dispute = _fixture.Build<DisputeCommand>()
            .With(x => x.Id, Guid.NewGuid().ToString())
            .With(x => x.OrderId, orderId)
            .Create();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", dispute);
        _factory.UnitOfWork.Disputes.GetDbContext().ChangeTracker.Clear();
        timer.Start();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    public static IEnumerable<object[]> GetValidationTextObjs()
    {
        yield return new object[]
        {
            _fixture.Build<DisputeCommand>()
                .Without(x => x.OrderId).Create()
        };
        yield return new object[]
        {
            _fixture.Build<DisputeCommand>()
                .Without(x => x.Details).Create()
        };
    }
}