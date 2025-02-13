using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.CartModule.MongoDb;

internal abstract class MongoContext<T>(IMongoDatabase database, string collectionName) : IMongoContext<T>
    where T : class
{
    public IMongoCollection<T> Collection { get; } = database.GetCollection<T>(collectionName);
}