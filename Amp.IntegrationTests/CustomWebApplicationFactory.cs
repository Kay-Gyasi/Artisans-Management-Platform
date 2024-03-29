﻿using Amp.IntegrationTests.Helpers;
using Amp.IntegrationTests.Stubs;
using AMP.Processors.Commands.UserManagement;
using AMP.Processors.Exceptions;
using AMP.Processors.Messaging;
using AMP.Processors.Repositories.Base;
using AMP.Processors.Workers.BackgroundWorker;

namespace Amp.IntegrationTests;

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
{
    private IServiceScope _scope;
    private AmpDbContext _db;

    public IUnitOfWork UnitOfWork { get; set; }
    public IDapperContext Dapper { get; set; }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, conf) =>
        {
            conf.Sources.Clear();
            conf.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"));
            conf.AddEnvironmentVariables();
            conf.Build();
        });
        // builder.ConfigureTestServices(services =>
        // {
        //     // We can further customize our application setup here.
        // });
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AmpDbContext>));

            services.Remove(descriptor);
            services.AddDbContext<AmpDbContext>(options =>
            {
                options.UseSqlServer(StartupHelper.ConnectionString);
            });
            services.Remove(services.SingleOrDefault(d => 
                d.ServiceType.FullName == StartupHelper.RateLimiterName));
            services.AddRateLimitingStub();
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IDapperContext)));
            services.AddScoped<IDapperContext, DapperContextStub>();
            services.Remove(services.SingleOrDefault(d => d.ServiceType == typeof(IBackgroundWorker)));
            services.AddScoped<IBackgroundWorker, BackgroundWorkerStub>();
            
            var sp = services.BuildServiceProvider();
            _scope = sp.CreateScope();
            var scopedServices = _scope.ServiceProvider;

            sp = services.BuildServiceProvider();
            _scope = sp.CreateScope();
            _db = scopedServices.GetRequiredService<AmpDbContext>();
            UnitOfWork = scopedServices.GetRequiredService<IUnitOfWork>();
            Dapper = scopedServices.GetRequiredService<IDapperContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            _db.Database.EnsureDeleted();
            _db.Database.Migrate();

            try
            {
                if (!File.Exists(StartupHelper.StartupScriptsPath)) return;
                var scripts = File.ReadAllText(StartupHelper.StartupScriptsPath);
                _db.Database.ExecuteSqlRaw(scripts
                    .Replace("\n", Environment.NewLine)
                    .Replace("\r", "")
                    .Replace("GO", ""));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database. Error: {Message}", ex.Message);
            }
        });
    }
    
    public async Task AuthenticateAsync(HttpClient testClient, UserType type)
    {
        testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",
            await GetJwtAsync(testClient, type));
    }

    private static async Task<string> GetJwtAsync(HttpClient testClient, UserType type)
    {
        return type switch
        {
            UserType.Artisan => await GetTokenForArtisanTestClient(testClient),
            UserType.Customer => await GetTokenForCustomerTestClient(testClient),
            UserType.Developer => null,
            UserType.Administrator => null,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static async Task<string> GetTokenForArtisanTestClient(HttpClient testClient)
    {
        var request = await testClient.PostAsJsonAsync("api/v1/users/login", new SigninCommand
        {
            Phone = "0557833216",
            Password = "kofi"
        }, new CancellationToken());
        return (await request.Content.ReadFromJsonAsync<SigninResponse>())?.Token ?? throw new InvalidIdException();
    }
    
    private static async Task<string> GetTokenForCustomerTestClient(HttpClient testClient)
    {
        var request = await testClient.PostAsJsonAsync("api/v1/users/login", new SigninCommand
        {
            Phone = "0207733247",
            Password = "pass"
        }, new CancellationToken());
        return (await request.Content.ReadFromJsonAsync<SigninResponse>())?.Token ?? throw new InvalidIdException();
    }

    protected override void Dispose(bool disposing)
    {
        _scope?.Dispose();
        _db?.Dispose();
    }
}