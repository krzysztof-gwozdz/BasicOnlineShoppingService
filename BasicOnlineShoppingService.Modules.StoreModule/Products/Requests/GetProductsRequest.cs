namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;

internal class GetProductsRequest : IRequest
{
    public Task<Product[]> Handle(CancellationToken cancellationToken)
    {
        var products = new[]
        {
            Product.Create("Cheap product", "This is a cheap product", 1.99m, "other"),
            Product.Create("Normal product", "This is a normal product", 9.99m, "other"),
            Product.Create("Expensive product", "This is an expensive product", 99.99m, "other"),
            
            Product.Create("Apple", "This is an apple", 1.99m, "food"),
            Product.Create("Banana", "This is a banana", 0.99m, "food"),
            Product.Create("Orange", "This is an orange", 1.49m, "food"),
        };
        return Task.FromResult(products);
    }
}