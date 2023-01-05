using AMP.Services;
using Microsoft.Extensions.FileProviders;
using AMP.Processors.HealthChecks;
using AMP.Processors.Hubs;
using AMP.WebApi.Installers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ILogger = Serilog.ILogger;

namespace AMP.WebApi;

public static class DependencyInjection
{
    public static void AddAmp(this IServiceCollection services, IConfiguration configuration,
        ILogger logger)
    {
        services.AddPersistence(configuration, logger)
            .AddProcessors()
            .RegisterServices(configuration)
            .AddMediatr()
            .AddDefaultConfig()
            .AddSwaggerConfig()
            .AddHttpContextAccessor()
            .AddAuthentication(configuration)
            .AddRateLimiting()
            .InstallCors()
            .InstallSignalR();
    }

    private static IServiceCollection AddDefaultConfig(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
        services.AddEndpointsApiExplorer();
        return services;
    }
    
    public static WebApplication AddApplicationBuilder(this WebApplicationBuilder builder, ILogger logger)
    {
        builder.Host.UseSerilog(logger);
        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(logger);

        var app = builder.Build();
        return app;
    }

    public static void ConfigurePipeline(this WebApplication app)
    {
        app.UseSwagger();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tukofix API v1");
            c.RoutePrefix = string.Empty;
        });

        app.UseHealthChecks("/health", new HealthCheckOptions()
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                await context.Response.BuildHealthCheckResponse(report);
            }
        });

        app.UseHttpsRedirection();
        app.UseExceptionHandler("/error");
        app.UseDefaultFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
            RequestPath = new PathString("/Files")
        });

        app.UseSerilogRequestLogging();

        app.UseRouting();

        app.UseCors();

        app.MapHub<DataCountHub>("/data-count");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRateLimiter();

        app.MapControllers();
        
        app.Run();
    }
}