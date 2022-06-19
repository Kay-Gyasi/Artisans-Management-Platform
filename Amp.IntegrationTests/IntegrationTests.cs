using AMP.Persistence.Database;
using AMP.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Amp.IntegrationTests;

public class IntegrationTests : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _testClient;
    private readonly IConfiguration _configuration;
    private readonly AmpDbContext _dbContext;
    private readonly IServiceScope _scope;
    protected IntegrationTests()
    {
        // Read on WebApplicationFactory
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.UseContentRoot(Directory.GetCurrentDirectory());
                builder.ConfigureServices(services =>
                {
                    
                    //services.RemoveAll(typeof(AmpDbContext));
                    //services.AddDbContext<AmpDbContext>(options =>
                    //{
                    //    options.UseSqlServer(_configuration.GetConnectionString("AMPTestDb"));
                    //});

                    //var sp = services.BuildServiceProvider();

                    //using (var scope = sp.CreateScope())
                    //{
                    //    var scopedServices = scope.ServiceProvider;
                    //    var db = scopedServices.GetRequiredService<AmpDbContext>();
                    //    db.Database.EnsureCreated();
                    //}
                });
            });

        _testClient = _factory.CreateClient();
        _scope = _factory.Services.CreateScope();
        _dbContext = GetService<AmpDbContext>();
    }

    public T GetItem<T>() where T : class
    {
        return _dbContext.Set<T>().FirstOrDefault();
    }

    protected async Task AuthenticateAsync()
    {
        _testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            await GetToken());
    }

    // Write Generic http methods to be used in tests


    private async Task<string> GetToken()
    {
        // Add ClientId, ClientSecret, GrantType, and Scope to the string content
        var response = await _testClient.PostAsync("/api/auth/login",
            new StringContent("{\"username\":\"admin\",\"password\":\"admin\"}"));
        return response.Headers.GetValues("Authorization").FirstOrDefault() ?? "";
    }

    private T GetService<T>()
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }

    public void Dispose()
    {
        _scope.Dispose();
        _testClient.Dispose();
        _dbContext.Dispose();
    }
}