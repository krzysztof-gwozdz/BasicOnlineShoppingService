using BasicOnlineShoppingService.App;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mongo2Go;

namespace BasicOnlineShoppingService.Modules.StoreModule.Tests;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbRunner _mongoDbRunner;

    public TestWebApplicationFactory()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        _mongoDbRunner = MongoDbRunner.Start();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.User.json", true)
            .AddEnvironmentVariables()
            .Build();
        builder.ConfigureAppConfiguration(configurationBuilder =>
            configurationBuilder.AddConfiguration(configuration)
                .AddInMemoryCollection(new Dictionary<string, string?> { ["MongoDb:ConnectionString"] = _mongoDbRunner.ConnectionString }));
        builder.UseEnvironment(Environments.Development);
        builder.ConfigureTestServices(services =>
        {
            services.AddMassTransitTestHarness(testHarnessConfig =>
            {
                testHarnessConfig.AddDelayedMessageScheduler();
                testHarnessConfig.UsingInMemory((context, configurator) =>
                {
                    configurator.UseDelayedMessageScheduler();
                    configurator.ConfigureEndpoints(context);
                });
            });
        });
    }

    protected override void Dispose(bool disposing)
    {
        _mongoDbRunner.Dispose();
        base.Dispose(disposing);
    }
}