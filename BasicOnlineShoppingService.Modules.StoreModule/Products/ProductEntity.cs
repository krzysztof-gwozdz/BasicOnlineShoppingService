using MongoDB.Bson.Serialization.Attributes;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products;

public class ProductEntity(Guid id, string name, string description, decimal price, string category)
{
    [BsonId]
    public Guid Id { get; init; } = id;

    public string Name { get; init; } = name;

    public string Description { get; init; } = description;

    public decimal Price { get; init; } = price;

    public string Category { get; init; } = category;
}