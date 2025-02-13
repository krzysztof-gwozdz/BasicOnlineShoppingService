using BasicOnlineShoppingService.Modules.CartModule.MongoDb;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts;

internal class CartsMongoContext(IMongoDatabase database) : MongoContext<CartEntity>(database, "carts");