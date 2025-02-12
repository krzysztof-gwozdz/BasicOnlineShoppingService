using BasicOnlineShoppingService.Modules.StoreModule.Products;
using Shouldly;
using Xunit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Tests.Products;

public class ProductTests
{ 
    [Fact]
    public void Creating_product_with_correct_values_creates_product_with_all_values_set()
    {
        // arrange
        const string name = "Product 1";
        const string description = "Description 1";
        const int price = 100;
        const string category = "Category 1";

        // act
        var product = Product.Create(name, description, price, category);

        // assert
        product.Id.ShouldNotBe(Guid.Empty);
        product.Name.ShouldBe(name);
        product.Description.ShouldBe(description);
        product.Price.ShouldBe(price);
        product.Category.ShouldBe(category);
    }
    
    [Fact]
    public void Creating_product_with_empty_name_throws_exception()
    {
        // arrange
        const string name = "";
        const string description = "Description 1";
        const int price = 100;
        const string category = "Category 1";

        // act
        var create = () => Product.Create(name, description, price, category);

        // assert
        create.ShouldThrow<ValidationException>().Message.ShouldBe("Name cannot be empty");
    }
    
    [Fact]
    public void Creating_product_with_empty_description_throws_exception()
    {
        // arrange
        const string name = "Product 1";
        const string description = "";
        const int price = 100;
        const string category = "Category 1";

        // act
        var create = () => Product.Create(name, description, price, category);

        // assert
        create.ShouldThrow<ValidationException>().Message.ShouldBe("Description cannot be empty");
    }
    
    [Fact]
    public void Creating_product_with_negative_price_throws_exception()
    {
        // arrange
        const string name = "Product 1";
        const string description = "Description 1";
        const int price = -100;
        const string category = "Category 1";

        // act
        var create = () => Product.Create(name, description, price, category);

        // assert
        create.ShouldThrow<ValidationException>().Message.ShouldBe("Price cannot be negative");
    }
    
    [Fact]
    public void Creating_product_with_empty_category_throws_exception()
    {
        // arrange
        const string name = "Product 1";
        const string description = "Description 1";
        const int price = 100;
        const string category = "";

        // act
        var create = () => Product.Create(name, description, price, category);

        // assert
        create.ShouldThrow<ValidationException>().Message.ShouldBe("Category cannot be empty");
    }
}