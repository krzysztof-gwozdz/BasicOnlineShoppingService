using BasicOnlineShoppingService.Modules.CartModule;
using BasicOnlineShoppingService.Modules.OrderModule;
using BasicOnlineShoppingService.Modules.Report;
using BasicOnlineShoppingService.Modules.StoreModule;

namespace BasicOnlineShoppingService.App;

public class Startup(IConfiguration configuration, IWebHostEnvironment environment)
{
    public void ConfigureServices(IServiceCollection services)
    {
        AddModules(services);
        services.AddControllers();
        services.AddConfiguredSwagger();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.UseConfiguredSwagger();
    }

    private static void AddModules(IServiceCollection services) =>
        services
            .AddCartModule()
            .AddOrderModule()
            .AddReportModule()
            .AddStoreModule();
}