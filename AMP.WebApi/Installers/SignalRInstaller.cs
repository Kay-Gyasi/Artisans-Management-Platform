using AMP.Processors.Hubs;

namespace AMP.WebApi.Installers;

public static class SignalRInstaller
{
    public static IServiceCollection InstallSignalR(this IServiceCollection services)
    {
        services.AddSignalR()
            .AddHubOptions<DataCountHub>(options =>
            {
                options.ClientTimeoutInterval = TimeSpan.FromMinutes(20);
                options.MaximumParallelInvocationsPerClient = 10;
            });
        return services;
    }
}