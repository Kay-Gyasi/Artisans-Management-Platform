using System.Text.Json;
using AMP.Processors.Responses;

namespace Amp.IntegrationTests.Tests;

public class OrderControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private static readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/orders";
    public OrderControllerTests(CustomWebApplicationFactory<Program> factory)
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
    public async Task GetPage_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
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

    #endregion

    #region GetSchedulePage

    [Fact]
    public async Task GetSchedulePage_WithoutAuth_ReturnsUnauthorized()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetSchedulePage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetSchedulePage_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var command = new PaginatedCommand
        {
            PageSize = 5
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetSchedulePage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    // TODO:: Optimize this test for speed
    [Fact]
    public async Task GetSchedulePage_ByArtisan_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetSchedulePage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().BeGreaterOrEqualTo(0);
        data?.Data.Count.Should().BeLessOrEqualTo(pageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }
    
    [Fact]
    public async Task GetSchedulePage_WithSearchTerm_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize,
            Search = "search term haha"
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetSchedulePage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().Be(0);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    #endregion
    
    #region GetRequestPage

    [Fact]
    public async Task GetRequestPage_WithoutAuth_ReturnsUnauthorized()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetRequestPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetRequestPage_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var command = new PaginatedCommand
        {
            PageSize = 5
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetRequestPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetRequestPage_ByArtisan_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetRequestPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().BeGreaterOrEqualTo(0);
        data?.Data.Count.Should().BeLessOrEqualTo(pageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task GetRequestPage_WithSearchTerm_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize,
            Search = "search term haha"
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetRequestPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().Be(0);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    #endregion
    
    #region GetHistoryPage

    [Fact]
    public async Task GetHistoryPage_WithoutAuth_ReturnsUnauthorized()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetHistoryPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetHistoryPage_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var command = new PaginatedCommand
        {
            PageSize = 5
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetHistoryPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetHistoryPage_ByArtisan_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetHistoryPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().BeGreaterOrEqualTo(0);
        data?.Data.Count.Should().BeLessOrEqualTo(pageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task GetHistoryPage_WithSearchTerm_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize,
            Search = "search term haha"
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetHistoryPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().Be(0);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    #endregion
    
    #region GetOrderHistoryPage

    [Fact]
    public async Task GetOrderHistoryPage_WithoutAuth_ReturnsUnauthorized()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetOrderHistoryPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetOrderHistoryPage_ByArtisan_ReturnsForbidden()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetOrderHistoryPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetOrderHistoryPage_ByCustomer_ReturnsOK()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetOrderHistoryPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().BeGreaterOrEqualTo(0);
        data?.Data.Count.Should().BeLessOrEqualTo(pageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task GetOrderHistoryPage_WithSearchTerm_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize,
            Search = "search term haha"
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetOrderHistoryPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().Be(0);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    #endregion
    
    #region GetCustomerOrderPage

    [Fact]
    public async Task GetCustomerOrderPage_WithoutAuth_ReturnsUnauthorized()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetCustomerOrderPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetCustomerOrderPage_ByArtisan_ReturnsForbidden()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetCustomerOrderPage", command);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(500);
    }
    
    [Fact]
    public async Task GetCustomerOrderPage_ByCustomer_ReturnsOK()
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
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetCustomerOrderPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().BeGreaterOrEqualTo(0);
        data?.Data.Count.Should().BeLessOrEqualTo(pageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task GetCustomerOrderPage_WithSearchTerm_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        const int pageSize = 20;
        var command = new PaginatedCommand
        {
            PageSize = pageSize,
            Search = "search term haha"
        };
        var timer = new Stopwatch();
    
        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetCustomerOrderPage", command);
        var data = await request.Content.ReadFromJsonAsync<PaginatedList<OrderPageDto>>();
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
        data?.Data.Count.Should().Be(0);
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
        var orderId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{orderId}");
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
        var orderId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{orderId}");
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
        var orderId = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{orderId}");
        var order = await request.Content.ReadFromJsonAsync<OrderDto>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        order.Should().NotBeNull();
        order?.Id.Should().Be(orderId);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    #endregion

    #region Insert

    [Fact]
    public async Task Insert_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var order = _fixture.Build<OrderCommand>()
            .Without(x => x.ReferenceNo)
            .Without(x => x.Id)
            .Without(x => x.ArtisanId)
            .Without(x => x.Cost)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Insert", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Fact]
    public async Task Insert_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var order = _fixture.Build<OrderCommand>()
            .Without(x => x.ReferenceNo)
            .Without(x => x.Id)
            .Without(x => x.ArtisanId)
            .Without(x => x.CustomerId)
            .Without(x => x.Cost)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Insert", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task Insert_ByCustomer_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var user = (await _factory.UnitOfWork.Users.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var service = (await _factory.UnitOfWork.Services.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var order = _fixture.Build<OrderCommand>()
            .With(x => x.UserId, user)
            .With(x => x.ServiceId, service)
            .Without(x => x.ReferenceNo)
            .Without(x => x.Id)
            .Without(x => x.ArtisanId)
            .Without(x => x.CustomerId)
            .Without(x => x.Cost)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Insert", order);
        var response = await request.Content.ReadFromJsonAsync<InsertOrderResponse>();
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        response?.Service.Should().NotBeNullOrEmpty();
        response?.OrderId.Should().NotBeNullOrEmpty();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Theory]
    [MemberData(nameof(GetValidationTextObjs))]
    public async Task Insert_WithoutRequiredInfo_ReturnsPreconditionFailed(OrderCommand order)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Insert", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    

    #endregion

    #region Save

    [Fact]
    public async Task Save_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var order = _fixture.Build<OrderCommand>()
            .Without(x => x.ReferenceNo)
            .Without(x => x.ArtisanId)
            .Without(x => x.Cost)
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
        var order = _fixture.Build<OrderCommand>()
            .Without(x => x.ReferenceNo)
            .Without(x => x.ArtisanId)
            .Without(x => x.CustomerId)
            .Without(x => x.Cost)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task Save_ByCustomer_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var user = (await _factory.UnitOfWork.Users.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var service = (await _factory.UnitOfWork.Services.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var order = _fixture.Build<OrderCommand>()
            .With(x => x.UserId, user)
            .With(x => x.ServiceId, service)
            .Without(x => x.ReferenceNo)
            .With(x => x.Id, id)
            .Without(x => x.ArtisanId)
            .Without(x => x.CustomerId)
            .Without(x => x.Cost)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Created);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task Save_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var user = (await _factory.UnitOfWork.Users.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var service = (await _factory.UnitOfWork.Services.GetBaseQuery().FirstOrDefaultAsync())?.Id;
        var id = Guid.NewGuid().ToString();
        var order = _fixture.Build<OrderCommand>()
            .With(x => x.UserId, user)
            .With(x => x.ServiceId, service)
            .Without(x => x.ReferenceNo)
            .With(x => x.Id, id)
            .Without(x => x.ArtisanId)
            .Without(x => x.CustomerId)
            .Without(x => x.Cost)
            .Create();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Theory]
    [MemberData(nameof(GetValidationTextObjs2))]
    public async Task Save_WithoutRequiredInfo_ReturnsPreconditionFailed(OrderCommand order)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/Save", order);
        timer.Start();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
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
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.Should().BeNull();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    #region UnassignArtisan

    [Fact]
    public async Task UnassignArtisan_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/UnassignArtisan/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Theory]
    [MemberData(nameof(GetUserTypes))]
    public async Task UnassignArtisan_WithInvalidId_ReturnsNotFound(UserType type)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, type);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/UnassignArtisan/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Theory]
    [MemberData(nameof(GetUserTypes))]
    public async Task UnassignArtisan_ValidId_ReturnsNoContent(UserType type)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, type);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x => x.ArtisanId != null))?.Id;

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/UnassignArtisan/{id}", null);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.ArtisanId.Should().BeNull();
        order.DateModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(5000));
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    #region Complete

    [Fact]
    public async Task Complete_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Complete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task Complete_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Complete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    } 
    
    [Fact]
    public async Task Complete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Complete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task Complete_ValidId_ArtisanComplete_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x=> !x.IsComplete && x.IsArtisanComplete))?.Id;

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Complete/{id}", null);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.IsComplete.Should().BeTrue();
        order.Status.Should().Be(OrderStatus.Completed);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task Complete_ValidId_ArtisanIncomplete_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x=> x.IsComplete == false && !x.IsArtisanComplete))?.Id;

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Complete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    

    #endregion

    #region SetCost

    [Fact]
    public async Task SetCost_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var command = _fixture.Build<SetCostCommand>().Create();

        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/SetCost", command);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }  
    
    [Fact]
    public async Task SetCost_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var command = _fixture.Build<SetCostCommand>().Create();

        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/SetCost", command);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    
    [Fact]
    public async Task SetCost_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var command = _fixture.Build<SetCostCommand>().Create();

        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/SetCost", command);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task SetCost_ValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x => x.Cost == 0))?.Id;
        var command = _fixture.Build<SetCostCommand>()
            .With(x => x.OrderId, id)
            .Create();

        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/SetCost", command);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.Cost.Should().Be(command.Cost);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    

    #endregion
    
    #region ArtisanComplete

    [Fact]
    public async Task ArtisanComplete_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/ArtisanComplete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task ArtisanComplete_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/ArtisanComplete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    } 
    
    [Fact]
    public async Task ArtisanComplete_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/ArtisanComplete/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task ArtisanComplete_ValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x=> !x.IsArtisanComplete))?.Id;

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/ArtisanComplete/{id}", null);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.IsArtisanComplete.Should().BeTrue();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    #endregion

    #region Accept

    [Fact]
    public async Task Accept_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Accept/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task Accept_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Accept/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    } 
    
    [Fact]
    public async Task Accept_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Accept/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task Accept_ValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x=> !x.IsRequestAccepted))?.Id;
    
        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/Accept/{id}", id);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();
    
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.IsRequestAccepted.Should().BeTrue();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    #endregion

    #region Cancel

    [Fact]
    public async Task Cancel_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Cancel/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task Cancel_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Cancel/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    } 
    
    [Fact]
    public async Task Cancel_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Cancel/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task Cancel_ValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x=> x.IsRequestAccepted))?.Id;

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/Cancel/{id}", null);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.IsRequestAccepted.Should().BeFalse();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    #endregion

    #region AssignArtisan

    [Fact]
    public async Task AssignArtisan_Anonymous_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();
        var artisanId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/AssignArtisan/{artisanId}/{id}",
            null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }    
    
    [Fact]
    public async Task AssignArtisan_ByArtisan_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();
        var artisanId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/AssignArtisan/{artisanId}/{id}",
            null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    } 
    
    [Fact]
    public async Task AssignArtisan_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();
        var artisanId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/AssignArtisan/{artisanId}/{id}", null);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
    [Fact]
    public async Task AssignArtisan_ValidId_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Orders.GetBaseQuery()
            .FirstOrDefaultAsync(x=> x.ArtisanId == null))?.Id;
        var artisanId = (await _factory.UnitOfWork.Artisans.GetBaseQuery()
            .FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.PutAsync($"{BaseUrl}/AssignArtisan/{artisanId}/{id}", null);
        _factory.UnitOfWork.Orders.GetDbContext().ChangeTracker.Clear();
        var order = await _factory.UnitOfWork.Orders.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        order.ArtisanId.Should().Be(artisanId);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }    
    
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
            _fixture.Build<OrderCommand>()
                .Without(x => x.UserId)
                .Create()
        };
        yield return new object[]
        {
            _fixture.Build<OrderCommand>()
                .Without(x => x.Description)
                .Create()
        };
        yield return new object[]
        {
            _fixture.Build<OrderCommand>()
                .Without(x => x.ServiceId)
                .Create()
        };
    }
    
    public static IEnumerable<object[]> GetValidationTextObjs2()
    {
        yield return new object[]
        {
            _fixture.Build<OrderCommand>()
                .Without(x => x.UserId)
                .Create()
        };
        yield return new object[]
        {
            _fixture.Build<OrderCommand>()
                .Without(x => x.Description)
                .Create()
        };
        yield return new object[]
        {
            _fixture.Build<OrderCommand>()
                .Without(x => x.ServiceId)
                .Create()
        };
        yield return new object[]
        {
            _fixture.Build<OrderCommand>()
                .Without(x => x.Id)
                .Create()
        };
    }
}