using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class AddProductRequest : IRequest
{
    public Task<Guid> Handle(AddProductDto addProductDto, CancellationToken cancellationToken)
    {
        var product = Product.Create(addProductDto.Name, addProductDto.Description, addProductDto.Price, addProductDto.Category);
        return Task.FromResult(product.Id);
    }
}