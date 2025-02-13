using MassTransit;

namespace BasicOnlineShoppingService.App;

public static class ServiceBusExtensions
{
    public static IServiceCollection AddServiceBus(this IServiceCollection services)
    {
        services.AddMassTransit(busRegistrationConfigurator => busRegistrationConfigurator.UsingInMemory());
        return services;
    }
}