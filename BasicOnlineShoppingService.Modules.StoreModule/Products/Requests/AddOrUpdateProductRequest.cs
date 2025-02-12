using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class AddOrUpdateProductRequest : IRequest
{
    public Task Handle(Guid id, AddProductDto productDto, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}