using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AMP.WebApi.Installers;

public static class AuthenticationInstaller
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var secretKey = configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts =>
            {
                opts.MapInboundClaims = true;
                opts.SaveToken = false;
                //opts.RequireHttpsMetadata = true;
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = key,
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.Name
                };
            });
        return services.AddAuthorizationPolicies();
    }

    private static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("CustomerOnlyResource", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context => context.User.IsInRole("Customer"));
            });    
            
            options.AddPolicy("ArtisanOnlyResource", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context => context.User.IsInRole("Artisan"));
            });    
            
            options.AddPolicy("CustomerAdminDevResource", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context => context.User.IsInRole("Customer")
                                                   || context.User.IsInRole("Administrator") 
                                                   || context.User.IsInRole("Developer"));
            });   
            
            options.AddPolicy("ArtisanAdminDevResource", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context => context.User.IsInRole("Artisan")
                                                    || context.User.IsInRole("Administrator") 
                                                    || context.User.IsInRole("Developer"));
            });   
            
            options.AddPolicy("AdminDevResource", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireAssertion(context => context.User.IsInRole("Administrator") 
                                                   || context.User.IsInRole("Developer"));
            });
        });
        return services;
    }

}