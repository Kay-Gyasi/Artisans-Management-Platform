namespace AMP.WebApi.Installers;

public static class RateLimitingInstaller
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 30,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }));
            options.AddPolicy("Unauthorized", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress,
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 3,
                        Window = TimeSpan.FromMinutes(1)
                    }));
            options.AddPolicy("Messaging", httpContext =>
                RateLimitPartition.GetSlidingWindowLimiter(partitionKey: httpContext.Connection.RemoteIpAddress,
                    partition => new SlidingWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 16,
                        SegmentsPerWindow = 4,
                        Window = TimeSpan.FromHours(1)
                    }));

            options.RejectionStatusCode = (int)HttpStatusCode.TooManyRequests;
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsync(
                        $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). " +
                        $"Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsync(
                        "Too many requests. Please try again later. " +
                        "Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
                }
            };
        });
        return services;
    }
}