using MongoDB.Bson.Serialization.Attributes;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts;

public class CartEntity(Guid id, string email, CartEntity.Product[] products)
{
    [BsonId]
    public Guid Id { get; init; } = id;

    public string Email { get; init; } = email;
    
    public Product[] Products { get; init; } = products;

    public class Product(Guid id, string name, decimal price, int quantity)
    {
        public Guid Id { get; init; } = id;

        public string Name { get; init; } = name;

        public decimal Price { get; init; } = price;

        public int Quantity { get; init; } = quantity;
    }
}