using BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;
using BasicOnlineShoppingService.Modules.StoreModule.Products.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products;

[ApiController]
[Route("api/v1/products")]
internal class ProductsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Get(
        [FromServices] GetProductsRequest getProductsRequest,
        CancellationToken cancellationToken)
    {
        var products = await getProductsRequest.Handle(cancellationToken);
        return Ok(new GetProductsDto(products));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(
        [FromServices] GetProductRequest getProductRequest,
        Guid id,
        CancellationToken cancellationToken)
    {
        var product = await getProductRequest.Handle(id, cancellationToken);
        return Ok(new GetProductDto(product));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Add(
        [FromServices] AddProductRequest addProductRequest,
        [FromBody] AddProductDto addProductDto,
        CancellationToken cancellationToken)
    {
        var productId = await addProductRequest.Handle(addProductDto, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = productId });
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> AddOrUpdate(
        [FromServices] AddOrUpdateProductRequest addOrUpdateProductRequest,
        Guid id,
        [FromBody] AddProductDto addProductDto,
        CancellationToken cancellationToken)
    {
        await addOrUpdateProductRequest.Handle(id, addProductDto, cancellationToken);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(
        [FromServices] DeleteProductRequest deleteProductRequest,
        Guid id,
        CancellationToken cancellationToken)
    {
        await deleteProductRequest.Handle(id, cancellationToken);
        return NoContent();
    }
}