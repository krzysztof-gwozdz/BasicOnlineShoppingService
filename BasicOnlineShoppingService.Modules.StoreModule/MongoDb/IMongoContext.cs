using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.MongoDb;

public interface IMongoContext<T>
{
    IMongoCollection<T> Collection { get; }
}