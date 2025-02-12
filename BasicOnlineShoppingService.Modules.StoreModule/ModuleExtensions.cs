namespace BasicOnlineShoppingService.Modules.StoreModule;

public static class ModuleExtensions
{
    public static IServiceCollection AddStoreModule(this IServiceCollection services)
    {
        return services.AddRequests();
    }
    
    private static IServiceCollection AddRequests(this IServiceCollection services) =>
        services.Scan(selector => selector
            .FromAssemblies(typeof(ModuleExtensions).Assembly)
            .AddClasses(implementationTypeFilter => implementationTypeFilter.AssignableTo<IRequest>(), false)
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );
}