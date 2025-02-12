using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class AddProductRequest(ProductsMongoContext productsMongoContext) : IRequest
{
    public async Task<Guid> Handle(AddProductDto addProductDto, CancellationToken cancellationToken)
    {
        var product = Product.Create(addProductDto.Name, addProductDto.Description, addProductDto.Price, addProductDto.Category);
        var productEntity = new ProductEntity(product.Id, product.Name, product.Description, product.Price, product.Category);
        await productsMongoContext.Collection.InsertOneAsync(productEntity, cancellationToken: cancellationToken);
        return product.Id;
    }
}