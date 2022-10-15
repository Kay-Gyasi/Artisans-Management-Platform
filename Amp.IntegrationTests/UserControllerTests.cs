namespace Amp.IntegrationTests;

public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public UserControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Login_ForVerifiedValidUser_ReturnsToken()
    {
        // Arrange
        var client = _factory.CreateClient();
        var command = new SigninCommand {Phone = "0207733247", Password = "pass"};
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync("api/v1/user/login", command);
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
        var client = _factory.CreateClient();
        var command = new SigninCommand {Phone = "0554860725", Password = "pass"};
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync("api/v1/user/login", command);
        timer.Stop();
        
        // Assert
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        request.StatusCode.Should().HaveFlag(HttpStatusCode.NoContent);
    }
    
    [Fact]
    public async Task Login_ForInvalidUser_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();
        var command = new SigninCommand {Phone = "0514856205", Password = "pass"};
        var timer = new Stopwatch();

        // Act
        timer.Start();
        var request = await client.PostAsJsonAsync("api/v1/user/login", command);
        timer.Stop();
        
        // Assert
        timer.ElapsedMilliseconds.Should().BeLessThan(2000);
        request.StatusCode.Should().HaveFlag(HttpStatusCode.NoContent);
    }
    
    private static IEnumerable<SigninCommand> ValidUserLoginData => 
        new List<SigninCommand>
        {
            new() {Phone = "0557833216", Password = "pass"},
            new() {Phone = "0207733247", Password = "pass"}
        };
}