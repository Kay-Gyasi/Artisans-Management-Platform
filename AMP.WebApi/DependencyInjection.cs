using Microsoft.OpenApi.Models;
using Serilog.Core;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AMP.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddAmp(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration)
            .AddRepositories()
            .AddProcessors()
            .RegisterAutoMapper()
            .AddMediatr()
            .AddCaching()
            .AddDefaultConfig();
        return services;
    }

    private static IServiceCollection AddDefaultConfig(this IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo {Title = "AMP API", Version = "v1"});
            //opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "AMP.WebApi.xml"));
            opt.CustomOperationIds(apiDescription => apiDescription.TryGetMethodInfo(out var methodInfo)
                ? methodInfo.Name
                : apiDescription.RelativePath);
        });

        return services;
    }

    public static WebApplication AddApplicationBuilder(this WebApplicationBuilder builder, Logger logger)
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
        }

        app.UseHttpsRedirection();

        app.UseSerilogRequestLogging();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}