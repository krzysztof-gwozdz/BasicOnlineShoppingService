using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Common.Exceptions;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Events;
using MassTransit;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class DeleteProductRequest(ProductsMongoContext productsMongoContext, IPublishEndpoint publishEndpoint) : IRequest
{
    public async Task Handle(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<ProductEntity>.Filter.Eq(productEntity => productEntity.Id, id);
        var product = await (await productsMongoContext.Collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        if (product is null)
        {
            throw new NotFoundException(nameof(ProductEntity), $"with id: {id}");
        }
        await productsMongoContext.Collection.DeleteOneAsync(filter, cancellationToken);
        await publishEndpoint.Publish(new ProductDeletedEvent(product.Id), cancellationToken);
    }
}