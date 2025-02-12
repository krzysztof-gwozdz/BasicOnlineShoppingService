namespace BasicOnlineShoppingService.Modules.StoreModule.Products;

internal class Product
{
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string Category { get; private set; }
    
    internal Product(Guid id, string name, string description, decimal price, string category)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }
    
    internal static Product Create(string name, string description, decimal price, string category)
    {
        Validate(name, description, price, category);
        return new Product(Guid.NewGuid(), name, description, price, category);
    }
    
    private static void Validate(string name, string description, decimal price, string category)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ValidationException(nameof(name), "Name cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ValidationException(nameof(description), "Description cannot be empty");
        }
        if (price < 0)
        {
            throw new ValidationException(nameof(price), "Price cannot be negative");
        }
        if (string.IsNullOrWhiteSpace(category))
        {
            throw new ValidationException(nameof(category), "Category cannot be empty");
        }
    }
}