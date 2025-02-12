using BasicOnlineShoppingService.Modules.StoreModule.MongoDb;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products;

internal class ProductsMongoContext(IMongoDatabase database) : MongoContext<ProductEntity>(database, "products");