using BasicOnlineShoppingService.Common.Exceptions;

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
        var errors = new ValidationErrorsSet();
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("invalid_name", "Name cannot be empty");
        }
        if (string.IsNullOrWhiteSpace(description))
        {
            errors.Add("invalid_description", "Description cannot be empty");
        }
        if (price < 0)
        {
            errors.Add("invalid_price", "Price cannot be negative");
        }
        if (string.IsNullOrWhiteSpace(category))
        {
            errors.Add("invalid_category", "Category cannot be empty");
        }
        if (errors.HasError)
            throw new ValidationException(errors);
    }
}