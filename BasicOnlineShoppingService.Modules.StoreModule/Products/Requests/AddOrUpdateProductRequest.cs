using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Events;
using MassTransit;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class AddOrUpdateProductRequest(ProductsMongoContext productsMongoContext, IPublishEndpoint publishEndpoint) : IRequest
{
    public async Task Handle(Guid id, AddProductDto addProductDto, CancellationToken cancellationToken)
    {
        var filter = Builders<ProductEntity>.Filter.Eq(productEntity => productEntity.Id, id);
        var existingProduct = await (await productsMongoContext.Collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        if (existingProduct is null)
        {
            var product = Product.Create(addProductDto.Name, addProductDto.Description, addProductDto.Price, addProductDto.Category);
            var productEntity = new ProductEntity(product.Id, product.Name, product.Description, product.Price, product.Category);
            await productsMongoContext.Collection.InsertOneAsync(productEntity, cancellationToken: cancellationToken);
            await publishEndpoint.Publish(new ProductAddedEvent(id), cancellationToken);
        }
        else
        {
            var update = Builders<ProductEntity>.Update
                .Set(productEntity => productEntity.Name, addProductDto.Name)
                .Set(productEntity => productEntity.Description, addProductDto.Description)
                .Set(productEntity => productEntity.Price, addProductDto.Price)
                .Set(productEntity => productEntity.Category, addProductDto.Category);
            await productsMongoContext.Collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await publishEndpoint.Publish(new ProductUpdatedEvent(id), cancellationToken);
        }
    }
}