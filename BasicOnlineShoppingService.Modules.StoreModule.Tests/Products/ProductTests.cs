using BasicOnlineShoppingService.Common.Exceptions;
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
    public void Creating_product_with_invalid_data_throws_exception()
    {
        // arrange
        const string name = "";
        const string description = "";
        const int price = -10;
        const string category = "";

        // act
        var create = () => Product.Create(name, description, price, category);

        // assert
        var exception = create.ShouldThrow<ValidationException>();
        exception.Message.ShouldBe("validation_error: An error occurred during validation.");
        exception.Errors.Count.ShouldBe(4);
        var nameError = exception.Errors.SingleOrDefault(x => x.Title == "invalid_name");
        nameError.ShouldNotBeNull();
        nameError.Detail.ShouldBe("Name cannot be empty");
        var descriptionError = exception.Errors.SingleOrDefault(x => x.Title == "invalid_description");
        descriptionError.ShouldNotBeNull();
        descriptionError.Detail.ShouldBe("Description cannot be empty");
        var priceError = exception.Errors.SingleOrDefault(x => x.Title == "invalid_price");
        priceError.ShouldNotBeNull();
        priceError.Detail.ShouldBe("Price cannot be negative");
        var categoryError = exception.Errors.SingleOrDefault(x => x.Title == "invalid_category");
        categoryError.ShouldNotBeNull();
        categoryError.Detail.ShouldBe("Category cannot be empty");
    }
}