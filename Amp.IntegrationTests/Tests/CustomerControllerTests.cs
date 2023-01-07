using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Dtos.UserManagement;

namespace Amp.IntegrationTests.Tests;

public class CustomerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private static readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/customers";
    public CustomerControllerTests(CustomWebApplicationFactory<Program> factory)
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
    
    #endregion

    #region Get

    [Fact]
    public async Task Get_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var customerId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{customerId}");
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
        var customerId = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{customerId}");
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
        var customerId = (await _factory.UnitOfWork.Customers.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{customerId}");
        var customer = await request.Content.ReadFromJsonAsync<CustomerDto>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        customer.Should().NotBeNull();
        customer?.Id.Should().Be(customerId);
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
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var userId = (await _factory.UnitOfWork.Users
            .GetBaseQuery()
            .FirstOrDefaultAsync(x => x.Contact.PrimaryContact == "0207733247"))?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/GetByUser");
        var customer = await request.Content.ReadFromJsonAsync<CustomerDto>();
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        customer.Should().NotBeNull();
        customer?.UserId.Should().Be(userId);
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
        var order = _fixture.Build<CustomerCommand>()
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
        var order = _fixture.Build<CustomerCommand>()
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
    public async Task Save_ByCustomer_ReturnsForbidden()
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var timer = new Stopwatch();
        var order = _fixture.Build<CustomerCommand>()
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
        var id = (await _factory.UnitOfWork.Customers.GetBaseQuery().FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/Delete/{id}");
        var customer = await _factory.UnitOfWork.Customers.GetAsync(id);
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        customer.Should().BeNull();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    
    
    public static IEnumerable<object[]> GetUserTypes()
    {
        yield return new object[]{UserType.Artisan};
        yield return new object[]{UserType.Customer};
    }
}