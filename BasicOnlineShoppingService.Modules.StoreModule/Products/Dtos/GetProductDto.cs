namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;

public record GetProductDto(Guid Id, string Name, string Description, decimal Price, string Category)
{
    internal GetProductDto(Product product) : this(product.Id, product.Name, product.Description, product.Price, product.Category)
    {
    }
}