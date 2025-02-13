namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Dtos;

public record GetCartDto(Guid Id, string Email, GetCartDto.Product[] Products)
{
    internal GetCartDto(Cart cart) : this(cart.Id, cart.Email, cart.Products.Select(p => new Product(p)).ToArray())
    {
    }
    
    public record Product(Guid Id, string Name, decimal Price, int Quantity)
    {
        internal Product(Cart.Product product) : this(product.Id, product.Name, product.Price, product.Quantity)
        {
        }
    }
}