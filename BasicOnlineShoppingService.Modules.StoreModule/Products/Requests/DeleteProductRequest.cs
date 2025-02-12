using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class DeleteProductRequest(ProductsMongoContext productsMongoContext) : IRequest
{
    public async Task Handle(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<ProductEntity>.Filter.Eq(productEntity => productEntity.Id, id);
        await productsMongoContext.Collection.FindOneAndDeleteAsync(filter, cancellationToken: cancellationToken);
    }
}