using BasicOnlineShoppingService.Modules.StoreModule.Products;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Tests.Products;

public class GetProductTests(TestWebApplicationFactory factory) : IntegrationTestBase(factory)
{
    private Task<HttpResponseMessage> Act(Guid productId) =>
        Get(GetUrl(productId));

    private static string GetUrl(Guid productId) =>
        $"/api/v1/products/{productId}";

    [Fact]
    public async Task Get_product_endpoint_called_with_id_of_existing_product_returns_http_status_code_ok()
    {
        // arrange
        var existingProduct = new ProductEntity(Guid.NewGuid(), "Product 1", "Description 1", 100, "Category 1");
        await InsertProductToDatabase(existingProduct);

        // act
        var response = await Act(existingProduct.Id);

        // assert
        response.AssertOk();
    }

    [Fact]
    public async Task Get_product_endpoint_called_with_id_of_existing_product_returns_correctly_mapped_data()
    {
        // arrange
        var existingProduct = new ProductEntity(Guid.NewGuid(), "Product 1", "Description 1", 100, "Category 1");
        await InsertProductToDatabase(existingProduct);

        // act
        var response = await Act(existingProduct.Id);

        // assert
        var productFromResponse = await response.GetContent<GetProductDto>();
        productFromResponse.ShouldNotBeNull();
        productFromResponse.Id.ShouldBe(existingProduct.Id);
        productFromResponse.Name.ShouldBe(existingProduct.Name);
        productFromResponse.Description.ShouldBe(existingProduct.Description);
        productFromResponse.Price.ShouldBe(existingProduct.Price);
        productFromResponse.Category.ShouldBe(existingProduct.Category);
    }

    [Fact]
    public async Task Get_product_endpoint_called_with_id_of_product_that_does_not_exist_returns_http_status_code_not_found()
    {
        // arrange
        var nonExistingProductId = Guid.NewGuid();

        // act
        var response = await Act(nonExistingProductId);

        // assert
        await response.AssertNotFound($"ProductEntity with id: {nonExistingProductId} not found.", GetUrl(nonExistingProductId));
    }

    private async Task InsertProductToDatabase(ProductEntity product) =>
        await Services.GetRequiredService<ProductsMongoContext>()
            .Collection
            .InsertOneAsync(product);
}