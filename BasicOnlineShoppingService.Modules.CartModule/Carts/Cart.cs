using BasicOnlineShoppingService.Common.Exceptions;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts;

internal class Cart
{
    public Guid Id { get; init; }

    public string Email { get; init; }
    public Product[] Products { get; init; }

    internal Cart(Guid id, string email, Product[] products)
    {
        Id = id;
        Email = email;
        Products = products;
    }

    internal static Cart Create(string email)
    {
        Validate(email);
        return new Cart(Guid.NewGuid(), email, []);
    }

    private static void Validate(string email)
    {
        var errors = new ValidationErrorsSet();
        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add("invalid_email", "Email cannot be empty");
        }
        if (errors.HasError)
            throw new ValidationException(errors);
    }

    internal class Product
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        internal Product(Guid id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        internal static Product Create(Guid id, string name, decimal price, int quantity)
        {
            Validate(id, name, price, quantity);
            return new Product(id, name, price, quantity);
        }
        
        private static void Validate(Guid id, string name, decimal price, int quantity)
        {
            var errors = new ValidationErrorsSet();
            if (id == Guid.Empty)
            {
                errors.Add("invalid_id", "Id cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add("invalid_name", "Name cannot be empty");
            }
            if (price < 0)
            {
                errors.Add("invalid_price", "Price cannot be negative");
            }
            if (quantity <= 0)
            {
                errors.Add("invalid_quantity", "Quantity must be greater than 0");
            }
            if (errors.HasError)
                throw new ValidationException(errors);
        }
    }
}