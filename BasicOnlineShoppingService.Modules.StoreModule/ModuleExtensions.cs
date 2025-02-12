using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Modules.StoreModule.MongoDb;

namespace BasicOnlineShoppingService.Modules.StoreModule;

public static class ModuleExtensions
{
    public static IServiceCollection AddStoreModule(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddRequests().AddMongoDb(configuration);
    }
    
    private static IServiceCollection AddRequests(this IServiceCollection services) =>
        services.Scan(selector => selector
            .FromAssemblies(typeof(ModuleExtensions).Assembly)
            .AddClasses(implementationTypeFilter => implementationTypeFilter.AssignableTo<IRequest>(), false)
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
}