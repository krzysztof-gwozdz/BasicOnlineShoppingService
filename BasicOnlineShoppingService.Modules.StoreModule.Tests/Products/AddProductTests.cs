using BasicOnlineShoppingService.Modules.StoreModule.Products;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Events;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Shouldly;
using Xunit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Tests.Products;

public class AddProductTests(TestWebApplicationFactory factory) : IntegrationTestBase(factory)
{
    private Task<HttpResponseMessage> Act(AddProductDto dto) =>
        Post(GetUrl(), dto);

    private static string GetUrl() =>
        "/api/v1/products";

    [Fact]
    public async Task Add_product_endpoint_called_with_correct_data_returns_http_status_code_created()
    {
        // arrange
        var productDto = new AddProductDto("Product 1", "Description 1", 100, "Category 1");

        // act
        var response = await Act(productDto);

        // assert
        response.AssertCreated();
    }

    [Fact]
    public async Task Add_product_endpoint_called_with_correct_data_returns_response_with_location_header()
    {
        // arrange
        var productDto = new AddProductDto("Product 1", "Description 1", 100, "Category 1");

        // act
        var response = await Act(productDto);

        // assert
        response.Headers.Location.ShouldNotBeNull();
    }

    [Fact]
    public async Task Add_product_endpoint_called_with_correct_data_adds_product_to_database()
    {
        // arrange
        var productDto = new AddProductDto("Product 1", "Description 1", 100, "Category 1");

        // act
        var response = await Act(productDto);

        // assert
        var productId = GetIdFromLocalization(response);
        var product = await GetProductFromDatabase(productId);
        product.ShouldNotBeNull();
        product.Name.ShouldBe(productDto.Name);
        product.Description.ShouldBe(productDto.Description);
        product.Price.ShouldBe(productDto.Price);
        product.Category.ShouldBe(productDto.Category);
    }

    [Fact]
    public async Task Add_product_endpoint_called_with_correct_data_emits_product_added_event()
    {
        // arrange
        var productDto = new AddProductDto("Product 1", "Description 1", 100, "Category 1");

        // act
        var response = await Act(productDto);

        // assert
        var productId = GetIdFromLocalization(response);
        var productAddedEvent = TestHarness.Published.Select<ProductAddedEvent>().SingleOrDefault(@event => @event.Context.Message.ProductId == productId);
        productAddedEvent.ShouldNotBeNull();
    }

    [Fact]
    public async Task Add_product_endpoint_called_with_invalid_data_returns_http_status_code_unprocessable_entity()
    {
        // arrange
        var productDto = new AddProductDto("", "", -10, "");

        // act
        var response = await Act(productDto);

        // assert
        await response.AssertUnprocessableEntity(GetUrl());
    }

    private async Task<ProductEntity> GetProductFromDatabase(Guid productId) =>
        await Services.GetRequiredService<ProductsMongoContext>()
            .Collection
            .Find(productEntity => productEntity.Id == productId)
            .FirstOrDefaultAsync();

    private static Guid GetIdFromLocalization(HttpResponseMessage response)
    {
        var location = response.Headers.Location;
        location.ShouldNotBeNull();
        var id = location.Segments.Last();
        Guid.TryParse(id, out var productId).ShouldBeTrue();
        return productId;
    }
}