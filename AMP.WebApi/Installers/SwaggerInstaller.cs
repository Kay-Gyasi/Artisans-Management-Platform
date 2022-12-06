using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AMP.WebApi.Installers;

public static class SwaggerInstaller
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tukofix API",
                Version = "1",
                Contact = new OpenApiContact
                {
                    Name = "Kofi Gyasi",
                    Email = "kofigyasidev@gmail.com",
                    Url = new Uri("https://kaygyasi.vercel.app/")
                }
            });
            opt.CustomOperationIds(apiDescription => apiDescription.TryGetMethodInfo(out var methodInfo)
                ? methodInfo.Name
                : apiDescription.RelativePath);
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization using the bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {new OpenApiSecurityScheme{Reference = new OpenApiReference()
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }}, new List<string>()}
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opt.IncludeXmlComments(xmlPath);
            
            // manipulate examples (requests and responses)
        });
        return services;
    }
}