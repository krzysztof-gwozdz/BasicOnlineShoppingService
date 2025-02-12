namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class DeleteProductRequest : IRequest
{
    public Task Handle(Guid id, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}