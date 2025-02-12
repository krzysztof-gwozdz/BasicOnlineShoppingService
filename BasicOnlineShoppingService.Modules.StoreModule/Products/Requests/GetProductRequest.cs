using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class GetProductRequest(ProductsMongoContext productsMongoContext) : IRequest
{
    public async Task<Product> Handle(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<ProductEntity>.Filter.Eq(productEntity => productEntity.Id, id);
        var product = await (await productsMongoContext.Collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        return new Product(product.Id, product.Name, product.Description, product.Price, product.Category);
    }
}