﻿using AMP.Services;
using Microsoft.Extensions.FileProviders;
using AMP.Processors.HealthChecks;
using AMP.WebApi.Installers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ILogger = Serilog.ILogger;

namespace AMP.WebApi;

public static class DependencyInjection
{
    public static void AddAmp(this IServiceCollection services, IConfiguration configuration,
        ILogger logger)
    {
        services.AddPersistence(configuration)
            .AddRepositories(logger)
            .AddProcessors()
            .RegisterAutoMapper()
            .AddDbHealthChecks()
            .AddMediatr()
            .AddCaching()
            .AddDefaultConfig()
            .AddSwaggerConfig()
            .AddMemoryCache()
            .RegisterInfrastructure(configuration)
            .AddAuthentication(configuration)
            .AddSmsMessaging()
            .AddRateLimiting()
            .AddWorkers()
            .InstallCors();
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
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
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

        app.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
            RequestPath = new PathString("/Files")
        });

        app.UseSerilogRequestLogging();

        app.UseRouting();

        app.UseCors();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseRateLimiter();

        app.MapControllers();
        
        app.Run();
    }
}