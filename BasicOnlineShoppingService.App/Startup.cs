using BasicOnlineShoppingService.App.Api;
using BasicOnlineShoppingService.App.Api.ErrorHandling;
using BasicOnlineShoppingService.Modules.CartModule;
using BasicOnlineShoppingService.Modules.OrderModule;
using BasicOnlineShoppingService.Modules.Report;
using BasicOnlineShoppingService.Modules.StoreModule;

namespace BasicOnlineShoppingService.App;

public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
{
    public void ConfigureServices(IServiceCollection services)
    {
        AddModules(services, configuration);
        services.AddErrorHandling(environment);
        services.AddControllers().ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });
        services.AddConfiguredSwagger();
        services.AddServiceBus();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseErrorHandling();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.UseConfiguredSwagger();
    }

    private static void AddModules(IServiceCollection services, IConfiguration configuration) =>
        services
            .AddCartModule()
            .AddOrderModule()
            .AddReportModule()
            .AddStoreModule(configuration);
}