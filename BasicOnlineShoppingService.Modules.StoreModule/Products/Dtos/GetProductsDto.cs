using System.Collections.Immutable;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;

public record GetProductsDto(IReadOnlyCollection<GetProductsDto.Product> Products)
{
    internal GetProductsDto(Products.Product[] products) : this(products.Select(product => new Product(product)).ToImmutableList())
    {
    }

    public record Product(Guid Id, string Name, string Description, decimal Price, string Category)
    {
        internal Product(Products.Product product) : this(product.Id, product.Name, product.Description, product.Price, product.Category)
        {
        }
    }
}