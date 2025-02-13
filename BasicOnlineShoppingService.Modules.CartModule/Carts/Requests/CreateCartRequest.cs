using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Modules.CartModule.Carts.Dtos;
using BasicOnlineShoppingService.Modules.CartModule.Carts.Events;
using MassTransit;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Requests;

internal class CreateCartRequest(CartsMongoContext cartsMongoContext, IPublishEndpoint publishEndpoint) : IRequest
{
    public async Task<Guid> Handle(CreateCartDto createCartDto, CancellationToken cancellationToken)
    {
        var cart = Cart.Create(createCartDto.Email);
        var cartEntity = new CartEntity(cart.Id, cart.Email, cart.Products.Select(product => new CartEntity.Product(product.Id, product.Name, product.Price, product.Quantity)).ToArray());
        await cartsMongoContext.Collection.InsertOneAsync(cartEntity, cancellationToken: cancellationToken);
        await publishEndpoint.Publish(new CartCreatedEvent(cart.Id), cancellationToken);
        return cart.Id;
    }
}