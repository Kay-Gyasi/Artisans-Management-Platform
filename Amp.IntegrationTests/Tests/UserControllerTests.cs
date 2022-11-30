namespace Amp.IntegrationTests.Tests;

//[TestCaseOrderer("Amp.IntegrationTests.Helpers.PriorityOrderer", "Amp.IntegrationTests")]
public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/users";
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Get_WithInvalidId_ReturnsNotFound()
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
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    //[Fact, TestPriority(0)]
    [Fact]
    public async Task Get_WithUnauthorizedUser_ReturnsUnauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = _fixture.Build<string>().Create();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/Get/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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
        var postUser = await client.PostAsJsonAsync($"api/v1/registrations/post", new UserCommand()
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
        userAfter?.DateModified.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        #endregion
    }
    
    [Fact]
    public async Task Update_WithInvalidUser_ReturnsNotFound()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        await _factory.AuthenticateAsync(client, UserType.Artisan);

        var command = _fixture.Build<UserCommand>()
            .With(x => x.Id, Guid.NewGuid().ToString())
            .Without(x => x.Image)
            .Create();

        // Act
        timer.Start();
        var response = await client.PutAsJsonAsync($"{BaseUrl}/update", command, new CancellationToken());
        timer.Stop();
        
        // Assert
        #region Assertions
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        #endregion
    }
    
    [Fact]
    public async Task Update_UnauthorizedUser_ReturnsUnauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient();
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
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    #region Delete

    [Fact]
    public async Task Delete_Unauthorized_ReturnsUnauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/delete/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    [Fact]
    public async Task DeleteArtisan_InvalidId_ReturnsNotFound()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/delete/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task DeleteArtisan_ValidId_ReturnsNoContent()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        await _factory.AuthenticateAsync(client, UserType.Artisan);
        const string phone = "0255463452";
        var user = _fixture.Build<UserCommand>()
            .With(x => x.FirstName, "Delete")
            .With(x => x.FamilyName, "Artisan")
            .With(x => x.Type, UserType.Artisan)
            .With(x => x.Contact, new ContactCommand {PrimaryContact = phone})
            .With(x => x.Password, "pass")
            .Without(x => x.Id)
            .Without(x => x.Image)
            .Without(x => x.Address)
            .Without(x => x.ImageId)
            .Without(x => x.Languages)
            .Without(x => x.IsRemoved)
            .Without(x => x.IsSuspended)
            .Without(x => x.MomoNumber)
            .Without(x => x.OtherName)
            .Without(x => x.LevelOfEducation)
            .Create();
        var initialRequest = await client.PostAsJsonAsync($"api/v1/registrations/post", user);
        var id = await _factory.UnitOfWork.Users.GetIdByPhone(phone);
        
        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/delete/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        var userInDb = await _factory.UnitOfWork.Users.GetIdByPhone(phone);
        userInDb.Should().BeNullOrEmpty();
        initialRequest.StatusCode.Should().Be(HttpStatusCode.Created);
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task DeleteCustomer_ValidId_ReturnsNoContent()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        const string phone = "0255637252";
        var user = _fixture.Build<UserCommand>()
            .With(x => x.FirstName, "Delete")
            .With(x => x.FamilyName, "Customer")
            .With(x => x.Type, UserType.Customer)
            .With(x => x.Contact, new ContactCommand {PrimaryContact = phone})
            .With(x => x.Password, "pass")
            .Without(x => x.Id)
            .Without(x => x.Image)
            .Without(x => x.Address)
            .Without(x => x.ImageId)
            .Without(x => x.Languages)
            .Without(x => x.IsRemoved)
            .Without(x => x.IsSuspended)
            .Without(x => x.MomoNumber)
            .Without(x => x.OtherName)
            .Without(x => x.LevelOfEducation)
            .Create();
        var initialRequest = await client.PostAsJsonAsync($"api/v1/registrations/post", user);
        var id = await _factory.UnitOfWork.Users.GetIdByPhone(phone);
        
        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/delete/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        var userInDb = await _factory.UnitOfWork.Users.GetIdByPhone(phone);
        userInDb.Should().BeNullOrEmpty();
        initialRequest.StatusCode.Should().Be(HttpStatusCode.Created);
        request.StatusCode.Should().Be(HttpStatusCode.NoContent);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task DeleteCustomer_InvalidId_ReturnsNotFound()
    {
        // Arrange
        using var client = _factory.CreateClient();
        var timer = new Stopwatch();
        await _factory.AuthenticateAsync(client, UserType.Customer);
        var id = Guid.NewGuid().ToString();

        // Act
        timer.Start();
        var request = await client.DeleteAsync($"{BaseUrl}/delete/{id}", new CancellationToken());
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    #region SendPasswordResetLink

    [Fact]
    public async Task SendPasswordResetLink_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        const string phone = "0345625435";
        
        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/SendPasswordResetLink/{phone}");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task SendPasswordResetLink_ValidId_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        const string phone = "0335463783";
        var user = _fixture.Build<UserCommand>()
            .With(x => x.FirstName, "RESET")
            .With(x => x.FamilyName, "Password")
            .With(x => x.Type, UserType.Customer)
            .With(x => x.Contact, new ContactCommand {PrimaryContact = phone})
            .With(x => x.Password, "pass")
            .Without(x => x.Id)
            .Without(x => x.Image)
            .Without(x => x.Address)
            .Without(x => x.ImageId)
            .Without(x => x.Languages)
            .Without(x => x.IsRemoved)
            .Without(x => x.IsSuspended)
            .Without(x => x.MomoNumber)
            .Without(x => x.OtherName)
            .Without(x => x.LevelOfEducation)
            .Create();
        var initialRequest = await client.PostAsJsonAsync($"api/v1/registrations/post", user);

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/SendPasswordResetLink/{phone}");
        timer.Stop();

        // Assert
        initialRequest.StatusCode.Should().Be(HttpStatusCode.Created);
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }

    #endregion

    #region ResetPassword

    [Fact]
    public async Task ResetPassword_ValidCredentials_ReturnsOK()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        const string phone = "0774566352";
        var user = _fixture.Build<UserCommand>()
            .With(x => x.FirstName, "RESET")
            .With(x => x.FamilyName, "Password2")
            .With(x => x.Type, UserType.Customer)
            .With(x => x.Contact, new ContactCommand {PrimaryContact = phone})
            .With(x => x.Password, "pass")
            .Without(x => x.Id)
            .Without(x => x.Image)
            .Without(x => x.Address)
            .Without(x => x.ImageId)
            .Without(x => x.Languages)
            .Without(x => x.IsRemoved)
            .Without(x => x.IsSuspended)
            .Without(x => x.MomoNumber)
            .Without(x => x.OtherName)
            .Without(x => x.LevelOfEducation)
            .Create();
        var initialRequest = await client.PostAsJsonAsync($"api/v1/registrations/post", user);
        var userInDb = await _factory.UnitOfWork.Users.GetByPhone(phone);
        var code = Encoding.UTF8.GetString(userInDb.PasswordKey)
            .RemoveSpecialCharacters();
        var command = new SigninCommand()
        {
            Phone = phone,
            Password = "kofi"
        };
        var verifyUser =
            await _factory.Dapper.Execute(
                $"UPDATE Users SET IsVerified = '1' WHERE Contact_PrimaryContact = '{phone}'",
                null, CommandType.Text);
        var initialLogin = await client.PostAsJsonAsync($"{BaseUrl}/Login", command, new CancellationToken());

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/ResetPassword/{phone}/{code}/kofi");
        timer.Stop();

        // Assert
        var subsequentLogin = await client.PostAsJsonAsync($"{BaseUrl}/Login", command, new CancellationToken());
        var response = await subsequentLogin.Content.ReadFromJsonAsync<SigninResponse>();

        initialRequest.StatusCode.Should().Be(HttpStatusCode.Created);
        verifyUser.Should().Be(1);
        initialLogin.StatusCode.Should().Be(HttpStatusCode.NoContent);
        subsequentLogin.StatusCode.Should().Be(HttpStatusCode.OK);
        response?.Token.Should().NotBeNullOrEmpty();
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task ResetPassword_InvalidCode_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        const string phone = "0334526635";
        var user = _fixture.Build<UserCommand>()
            .With(x => x.FirstName, "RESET")
            .With(x => x.FamilyName, "Password2")
            .With(x => x.Type, UserType.Customer)
            .With(x => x.Contact, new ContactCommand {PrimaryContact = phone})
            .With(x => x.Password, "pass")
            .Without(x => x.Id)
            .Without(x => x.Image)
            .Without(x => x.Address)
            .Without(x => x.ImageId)
            .Without(x => x.Languages)
            .Without(x => x.IsRemoved)
            .Without(x => x.IsSuspended)
            .Without(x => x.MomoNumber)
            .Without(x => x.OtherName)
            .Without(x => x.LevelOfEducation)
            .Create();
        var initialRequest = await client.PostAsJsonAsync($"api/v1/registrations/post", user);
        var userInDb = await _factory.UnitOfWork.Users.GetByPhone(phone);
        var code = _fixture.Build<string>().Create();
        var command = new SigninCommand()
        {
            Phone = phone,
            Password = "kofi"
        };
        var verifyUser =
            await _factory.Dapper.Execute(
                $"UPDATE Users SET IsVerified = '1' WHERE Contact_PrimaryContact = '{phone}'",
                null, CommandType.Text);
        
        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/ResetPassword/{phone}/{code}/kofi");
        timer.Stop();

        // Assert
        initialRequest.StatusCode.Should().Be(HttpStatusCode.Created);
        verifyUser.Should().Be(1);
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task ResetPassword_InvalidPhone_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        const string phone = "0234514256";
        var code = _fixture.Build<string>().Create();

        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/ResetPassword/{phone}/{code}/kofi");
        timer.Stop();

        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
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