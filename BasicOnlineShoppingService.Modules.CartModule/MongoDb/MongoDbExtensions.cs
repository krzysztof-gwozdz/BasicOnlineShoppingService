using BasicOnlineShoppingService.Modules.CartModule.Carts;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.CartModule.MongoDb;

internal static class MongoDbExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.TryRegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
        services.Configure<MongoDbOptions>(configuration.GetSection("StoreModule:MongoDb"));
        services.AddSingleton<IValidateOptions<MongoDbOptions>, MongoDbOptionsValidator>();
        services.AddSingleton<IMongoClient, MongoClient>(serviceProvider => 
            new(serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value.ConnectionString));
        services.AddSingleton(serviceProvider =>
            serviceProvider.GetRequiredService<IMongoClient>().GetDatabase(
                serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value.DatabaseName));
        services.AddSingleton<CartsMongoContext>();
        return services;
    }
}