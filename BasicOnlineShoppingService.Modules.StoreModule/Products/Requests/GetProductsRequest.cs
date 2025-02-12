using BasicOnlineShoppingService.Common;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class GetProductsRequest(ProductsMongoContext productsMongoContext) : IRequest
{
    public async Task<Product[]> Handle(CancellationToken cancellationToken)
    {
        var products = await (await productsMongoContext.Collection.FindAsync(_ => true, cancellationToken: cancellationToken)).ToListAsync(cancellationToken: cancellationToken);
        return products.Select(product => new Product(product.Id, product.Name, product.Description, product.Price, product.Category)).ToArray();
    }
}