using AMP.Processors.Commands.BusinessManagement;

namespace Amp.IntegrationTests.Tests.OrdersController;

public partial class OrderControllerTests
{
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
}