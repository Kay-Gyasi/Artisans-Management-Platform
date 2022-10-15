﻿namespace Amp.IntegrationTests;

public class RegistrationControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly Fixture _fixture = new();
    private const string BaseUrl = "api/v1/registration";

    public RegistrationControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #region PostUser
    [Fact]
    public async Task PostCustomer_WithValidInput_ReturnsOkObjectResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = GetPostUser().First();
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/post", user);
        timer.Stop();
        
        //Assert
        var userInDb = await _factory.UnitOfWork.Users.GetByPhone(user?.Contact.PrimaryContact);
        var userInCustomersDb = await _factory.UnitOfWork.Customers.GetByUserIdAsync(userInDb?.Id);
        var registration = await _factory.UnitOfWork.Registrations.GetByPhone(user?.Contact.PrimaryContact);

        #region Assertions
        userInDb.Should().NotBeNull();
        userInDb?.IsVerified.Should().BeFalse();
        userInDb?.DisplayName.Should().NotBeNullOrEmpty();
        userInDb?.Contact.PrimaryContact.Should().NotBeNullOrEmpty();
        userInDb?.Type.Should().Be(UserType.Customer);
        userInCustomersDb.Should().NotBeNull();
        registration.Should().NotBeNull();
        registration.Phone.Should().NotBeNullOrEmpty();
        registration.VerificationCode.Should().NotBeNullOrEmpty();
        request.StatusCode.Should().HaveFlag(HttpStatusCode.OK);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        #endregion
    }
    
    [Fact]
    public async Task PostArtisan_WithValidInput_ReturnsOkObjectResult()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = GetPostUser().Last();
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/post", user);
        timer.Stop();
        
        //Assert
        var userInDb = await _factory.UnitOfWork.Users.GetByPhone(user?.Contact.PrimaryContact);
        var userInArtisansDb = await _factory.UnitOfWork.Artisans.GetArtisanByUserId(userInDb?.Id);
        var registration = await _factory.UnitOfWork.Registrations.GetByPhone(user?.Contact.PrimaryContact);

        #region Assertions
        userInDb.Should().NotBeNull();
        userInDb?.IsVerified.Should().BeFalse();
        userInDb?.DisplayName.Should().NotBeNullOrEmpty();
        userInDb?.Contact.PrimaryContact.Should().NotBeNullOrEmpty();
        userInDb?.Type.Should().Be(UserType.Artisan);
        userInArtisansDb.Should().NotBeNull();
        userInArtisansDb.BusinessName.Should().NotBeNullOrEmpty();
        registration.Should().NotBeNull();
        registration.Phone.Should().NotBeNullOrEmpty();
        registration.VerificationCode.Should().NotBeNullOrEmpty();
        request.StatusCode.Should().HaveFlag(HttpStatusCode.OK);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        #endregion
    }
    
    [Theory]
    [MemberData(nameof(GetPostMissingValuesUsers))]
    public async Task PostUser_WithMissingInputs_ReturnsInternalServerError(UserCommand user)
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync($"{BaseUrl}/post", user);
        timer.Stop();

        //Assert
        timer.ElapsedMilliseconds.Should().BeLessThan(1250);
        request.IsSuccessStatusCode.Should().BeFalse();
        request.StatusCode.Should().HaveFlag(HttpStatusCode.InternalServerError);
    }
    #endregion


    #region VerifyPhone

    [Theory]
    [MemberData(nameof(GetUsersForVerifyTest))]
    public async Task Verify_WithCorrectCredentials_ReturnsOkResult(UserCommand user)
    {
        // Arrange
        var client = _factory.CreateClient();
        var postUser = await client.PostAsJsonAsync($"{BaseUrl}/post", user);
        var registration = await _factory.UnitOfWork.Registrations.GetByPhone(user.Contact.PrimaryContact);
        var timer = new Stopwatch();
        
        // Act
        timer.Start();
        var request =
            await client.GetAsync(
                $"{BaseUrl}/verify/{user.Contact.PrimaryContact}/{registration.VerificationCode}",
                new CancellationToken());
        timer.Stop();

        // Assert
        var isRegistrationDeleted = await _factory.UnitOfWork.Registrations.GetByPhone(user.Contact.PrimaryContact);
        var userInDb = await _factory.UnitOfWork.Users.GetByPhone(user.Contact.PrimaryContact);

        #region Assertions
        user.Should().NotBeNull();
        registration.Should().NotBeNull();
        postUser.IsSuccessStatusCode.Should().BeTrue();
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        isRegistrationDeleted.Should().BeNull();
        userInDb.Should().NotBeNull();
        userInDb.IsVerified.Should().BeTrue();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        #endregion
    }
    
    [Theory]
    [MemberData(nameof(GetUsersForWrongVerifyTest))]
    public async Task Verify_WithWrongCode_ReturnsNotFound(UserCommand user)
    {
        // Arrange
        var client = _factory.CreateClient();
        var postUser = await client.PostAsJsonAsync($"{BaseUrl}/post", user);
        var registration = await _factory.UnitOfWork.Registrations.GetByPhone(user.Contact.PrimaryContact);
        var timer = new Stopwatch();
        
        // Act
        timer.Start();
        var request =
            await client.GetAsync(
                $"{BaseUrl}/verify/{user.Contact.PrimaryContact}/12345678912345678912",
                new CancellationToken());
        timer.Stop();

        // Assert
        var isRegistrationDeleted = await _factory.UnitOfWork.Registrations.GetByPhone(user.Contact.PrimaryContact);
        var userInDb = await _factory.UnitOfWork.Users.GetByPhone(user.Contact.PrimaryContact);

        #region Assertions
        user.Should().NotBeNull();
        registration.Should().NotBeNull();
        postUser.IsSuccessStatusCode.Should().BeTrue();
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        isRegistrationDeleted.Should().NotBeNull();
        userInDb.Should().NotBeNull();
        userInDb.IsVerified.Should().BeFalse();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        #endregion
    }

    [Fact]
    public async Task Verify_WithInvalidPhone_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        
        // Act
        timer.Start();
        var request =
            await client.GetAsync(
                $"{BaseUrl}/verify/0456734526/12345678912345678912",
                new CancellationToken());
        timer.Stop();
        
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.NotFound);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task Verify_WithInvalidCodeLength_ReturnsInternalServerError()
    {
        // Arrange
        var client = _factory.CreateClient();
        var timer = new Stopwatch();
        
        // Act
        timer.Start();
        var request =
            await client.GetAsync(
                $"{BaseUrl}/verify/0456734526/invalid",
                new CancellationToken());
        timer.Stop();
        
        // Assert
        request.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        timer.ElapsedMilliseconds.Should().BeLessThan(1000);
    }
    #endregion


    #region SendCode

    [Fact]
    public async Task SendCode_WithValidPhone_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = new UserCommand
        {
            FirstName = "Send",
            FamilyName = "Code",
            Type = UserType.Artisan,
            Password = "oasis",
            Contact = new ContactCommand
            {
                PrimaryContact = "0344455666"
            }
        };
        var postUser = await client.PostAsJsonAsync($"{BaseUrl}/post", user);
        var registration = await _factory.UnitOfWork.Registrations.GetByPhone(user.Contact.PrimaryContact);
        var timer = new Stopwatch();
        
        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/SendCode/{user.Contact.PrimaryContact}");
        timer.Stop();
        
        // Assert
        postUser.IsSuccessStatusCode.Should().BeTrue();
        registration.Should().NotBeNull();
        registration.Phone.Should().Be(user.Contact.PrimaryContact);
        request.StatusCode.Should().Be(HttpStatusCode.OK);
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    
    [Fact]
    public async Task SendCode_WithInvalidPhone_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var phone = _fixture.Build<string>().Create();
        var timer = new Stopwatch();
        
        // Act
        timer.Start();
        var request = await client.GetAsync($"{BaseUrl}/SendCode/phone");
        timer.Stop();
        
        // Assert
        request.IsSuccessStatusCode.Should().BeFalse();
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
    }
    

    #endregion
    
    
    public static IEnumerable<object[]> GetPostMissingValuesUsers()
    {
        yield return new object[]
        {
            new UserCommand
            {
                FirstName = "Test",
                Type = UserType.Customer,
                Contact = new ContactCommand
                {
                    PrimaryContact = "0207141976"
                },
                Password = "pass"
            }
        };
        yield return new object[]
        {
            new UserCommand
            {
                FamilyName = "User",
                Type = UserType.Customer,
                Contact = new ContactCommand
                {
                    PrimaryContact = "0207141976"
                },
                Password = "pass"
            }
        };
        yield return new object[]
        {
            new UserCommand
            {
                FirstName = "Test",
                FamilyName = "User",
                Contact = new ContactCommand
                {
                    PrimaryContact = "0207141976"
                },
                Password = "pass"
            }
        };
        yield return new object[]
        {
            new UserCommand
            {
                FirstName = "Test",
                FamilyName = "User",
                Type = UserType.Customer,
                Password = "pass"
            }
        };
        yield return new object[]
        {
            new UserCommand
            {
                FirstName = "Test",
                FamilyName = "User",
                Type = UserType.Customer,
                Contact = new ContactCommand
                {
                    PrimaryContact = "0207141976"
                }
            }
        };
        yield return new object[]
        {
            new UserCommand
            {
                FirstName = "Test",
                FamilyName = "User",
                Type = UserType.Developer,
                Contact = new ContactCommand
                {
                    PrimaryContact = "0207141976"
                },
                Password = "pass"
            }
        };
        yield return new object[]
        {
            new UserCommand
            {
                //FirstName = "Test",
                FamilyName = "User",
                Type = UserType.Administrator,
                Contact = new ContactCommand
                {
                    PrimaryContact = "0207141976"
                },
                Password = "pass"
            }
        };
    }
    public static IEnumerable<object[]> GetUsersForVerifyTest()
    {
        yield return new object[]
        {
            new UserCommand {
                FirstName = "Ekua",
                FamilyName = "Gyasi",
                Type = UserType.Artisan,
                Password = "werty",
                Contact = new ContactCommand
                {
                    PrimaryContact = "0834536492"
                }
            }
        };
        yield return new object[]
        {
            new UserCommand {
                FirstName = "Kessewa",
                FamilyName = "Gyasi",
                Type = UserType.Customer,
                Password = "werty",
                Contact = new ContactCommand
                {
                    PrimaryContact = "0846536492"
                }
            }
        };
    }
    public static IEnumerable<object[]> GetUsersForWrongVerifyTest()
    {
        yield return new object[]
        {
            new UserCommand {
                FirstName = "Ekua",
                FamilyName = "Gyasi",
                Type = UserType.Artisan,
                Password = "werty",
                Contact = new ContactCommand
                {
                    PrimaryContact = "0834536562"
                }
            }
        };
        yield return new object[]
        {
            new UserCommand {
                FirstName = "Kessewa",
                FamilyName = "Gyasi",
                Type = UserType.Customer,
                Password = "werty",
                Contact = new ContactCommand
                {
                    PrimaryContact = "0362536492"
                }
            }
        };
    }
    private IEnumerable<UserCommand> GetPostUser()
    {
        return new[]
        {
            _fixture.Build<UserCommand>()
                .With(x => x.FirstName, "Test")
                .With(x => x.FamilyName, "Customer")
                .With(x => x.Type, UserType.Customer)
                .With(x => x.Contact, new ContactCommand {PrimaryContact = "0207141976"})
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
                .Create(),
            _fixture.Build<UserCommand>()
                .With(x => x.FirstName, "Test")
                .With(x => x.FamilyName, "Artisan")
                .With(x => x.Type, UserType.Artisan)
                .With(x => x.Contact, new ContactCommand {PrimaryContact = "0207151976"})
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
                .Create()
        };
    }
}