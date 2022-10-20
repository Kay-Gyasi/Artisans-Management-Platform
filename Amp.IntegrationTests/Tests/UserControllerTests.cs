namespace Amp.IntegrationTests.Tests;

public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/user";
    public UserControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #region Login
    [Fact]
    public async Task Login_ForVerifiedValidUser_ReturnsToken()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var command = new SigninCommand {Phone = "0207733247", Password = "pass"};
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/login", command);
        timer.Stop();
        var response = await request.Content.ReadFromJsonAsync<SigninResponse>();

        // Assert
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
        response.Should().NotBeNull();
        response?.Token.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task Login_ForUnverifiedValidUser_ReturnsNoContent()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var command = new SigninCommand {Phone = "0554860725", Password = "pass"};
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/login", command);
        timer.Stop();
        
        // Assert
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
        request.StatusCode.Should().HaveFlag(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Login_ForInvalidUser_ReturnsNoContent()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var command = new SigninCommand {Phone = "0514856205", Password = "pass"};
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/login", command);
        timer.Stop();
        
        // Assert
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
        request.StatusCode.Should().HaveFlag(HttpStatusCode.NoContent);
    }
    #endregion

    #region GetUserPage

    [Fact]
    public async Task GetUserPage_WithoutAuth_ReturnsUnauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient();
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
    public async Task GetUserPage_WithValidCommand_ReturnsOk()
    {
        // Arrange
        using var client = _factory.CreateClient();
        // TODO:: Implement that customers and artisans should not be able to call this endpoint
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
        var response = await request.Content.ReadFromJsonAsync<PaginatedList<UserPageDto>>();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        response?.Data.Should().NotBeNull();
        response?.PageSize.Should().Be(command.PageSize);
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }
    
    [Theory]
    [MemberData(nameof(GetPageSearchTerms))]
    public async Task GetUserPage_WithValidSearchTerms_ReturnsOk(PaginatedCommand command)
    {
        // Arrange
        using var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        timer.Stop();
        var response = await request.Content.ReadFromJsonAsync<PaginatedList<UserPageDto>>();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        response?.Data.Should().NotBeNull();
        response?.Data.Count.Should().BeGreaterThan(0);
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }
    
    [Fact]
    public async Task GetUserPage_WithInvalidSearchTerm_ReturnsOk()
    {
        // Arrange
        using var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var command = new PaginatedCommand
        {
            PageSize = 5,
            Search = "invalid hahahaha"
        };
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/GetPage", command);
        timer.Stop();
        var response = await request.Content.ReadFromJsonAsync<PaginatedList<UserPageDto>>();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        response?.Data.Should().NotBeNull();
        response?.Data.Count.Should().Be(0);
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }

    #endregion

    #region Get

    [Fact]
    public async Task Get_WithValidId_ReturnsOk()
    {
        // Arrange
        using var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = (await _factory.UnitOfWork.Users
            .GetBaseQuery()
            .FirstOrDefaultAsync())?.Id;

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{id}", new CancellationToken());
        timer.Stop();
        var response = await request.Content.ReadFromJsonAsync<UserDto>();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Should().NotBeNull();
        response?.Contact.Should().NotBeNull();
        response?.Contact.PrimaryContact.Should().NotBeNullOrEmpty();
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }
    
    [Fact]
    public async Task Get_WithInvalidId_ReturnsNoContent()
    {
        // Arrange
        using var client = _factory.CreateClient();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var timer = new Stopwatch();
        var id = _fixture.Build<string>().Create();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
    }
    
    [Fact]
    public async Task Get_WithUnauthorizedUser_ReturnsUnauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "unauthorized");
        var timer = new Stopwatch();
        var id = _fixture.Build<string>().Create();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    

    #endregion

    #region Update

    [Fact]
    public async Task Update_WithValidFullUser_ReturnsCreated()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var postUser = await client.PostAsJsonAsync($"api/v1/registration/post", new UserCommand()
        {
            FirstName = "Yet To",
            FamilyName = "Update",
            Type = UserType.Artisan,
            Password = "pass",
            Contact = new ContactCommand{ PrimaryContact = "0256735647"}
        });
        var userId = (await _factory.UnitOfWork.Users
            .GetBaseQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.FamilyName == "Update"))?.Id;

        var command = _fixture.Build<UserCommand>()
            .With(x => x.Id, userId)
            .With(x => x.FirstName, "Update")
            .With(x => x.FamilyName, "Me")
            .With(x => x.Contact, new ContactCommand {PrimaryContact = "0234567891"})
            .With(x => x.MomoNumber, "0234567892")
            .With(x => x.LevelOfEducation, LevelOfEducation.PhD)
            .With(x => x.Languages, new List<LanguagesCommand>{new() {Name = "English"}})
            .Without(x => x.Image)
            .Without(x => x.ImageId)
            .Create();

        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/update", command, new CancellationToken());
        timer.Stop();

        // Assert
        var userAfter = await _factory.UnitOfWork.Users.
                GetBaseQuery()
                .FirstOrDefaultAsync(x => x.FirstName == command.FirstName);

        #region Assertions
        request.StatusCode.Should().Be(HttpStatusCode.Created);
        userAfter?.Id.Should().Be(userId);
        userAfter?.FirstName.Should().Be(command.FirstName);
        userAfter?.FamilyName.Should().Be(command.FamilyName);
        userAfter?.DisplayName.Should().Be(command.DisplayName);
        userAfter?.OtherName.Should().Be(command.OtherName);
        userAfter?.Contact.PrimaryContact.Should().Be(command.Contact.PrimaryContact);
        userAfter?.MomoNumber.Should().Be(command.MomoNumber);
        userAfter?.LevelOfEducation.Should().Be(LevelOfEducation.PhD);
        userAfter?.Address.Should().NotBeNull();
        userAfter?.DateModified.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(-1));
        timer.ElapsedMilliseconds.Should().BeLessThan(5000);
        #endregion
    }
    
    [Fact]
    public async Task Update_UnauthorizedUser_ReturnsUnauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient();
        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", "unauthorized");
        var timer = new Stopwatch();
        var user = _fixture.Build<UserCommand>()
            .Without(x => x.Image)
            .Create();

        // Act
        timer.Start();
        var request = await client.PutAsJsonAsync($"{BaseUrl}/update", user, new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }

    #endregion
    
    
    public static IEnumerable<object[]> GetPageSearchTerms()
    {
        yield return new object[]
        {
            new PaginatedCommand
            {
                PageSize = 5,
                Search = "4"
            }
        };
        yield return new object[]
        {
            new PaginatedCommand
            {
                PageSize = 5,
                Search = "Kay"
            }
        };
        yield return new object[]
        {
            new PaginatedCommand
            {
                PageSize = 5,
                Search = "Unverified"
            }
        };
    }

}