var builder = WebApplication.CreateBuilder(args);
WebApplication app;
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

try
{
    logger.Information("App starting up...");
    builder.Services.AddAmp(builder.Configuration, logger);
    app = builder.AddApplicationBuilder(logger);
    logger.Information("App started");
}
catch (Exception e)
{
    logger.Fatal(e.Message);
    throw;
}
finally
{
    Log.CloseAndFlush();
}

app.ConfigurePipeline();

public partial class Program { }
