namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class GetProductRequest : IRequest
{
    public Task<Product> Handle(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(Product.Create("Your product", "This is your product", 0.01m, "fun"));
    }
}