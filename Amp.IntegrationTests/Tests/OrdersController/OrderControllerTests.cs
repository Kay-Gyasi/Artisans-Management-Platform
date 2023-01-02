namespace Amp.IntegrationTests.Tests.OrdersController;

public partial class OrderControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
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