using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.CartModule.MongoDb;

public interface IMongoContext<T>
{
    IMongoCollection<T> Collection { get; }
}