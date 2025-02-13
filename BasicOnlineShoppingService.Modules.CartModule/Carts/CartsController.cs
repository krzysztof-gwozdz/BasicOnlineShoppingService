using BasicOnlineShoppingService.Modules.CartModule.Carts.Dtos;
using BasicOnlineShoppingService.Modules.CartModule.Carts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts;

[ApiController]
[Route("api/v1/carts")]
internal class CartsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get(
        [FromServices] GetCartRequest getCartRequest,
        Guid id,
        CancellationToken cancellationToken)
    {
        var cart = await getCartRequest.Handle(id, cancellationToken);
        return Ok(new GetCartDto(cart));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Add(
        [FromServices] CreateCartRequest createCartRequest,
        [FromBody] CreateCartDto createCartDto,
        CancellationToken cancellationToken)
    {
        var cartId = await createCartRequest.Handle(createCartDto, cancellationToken);
        return CreatedAtAction(nameof(Get), new { id = cartId }, null);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(
        [FromServices] DeleteCartRequest deleteCartRequest,
        Guid id,
        CancellationToken cancellationToken)
    {
        await deleteCartRequest.Handle(id, cancellationToken);
        return NoContent();
    }
}