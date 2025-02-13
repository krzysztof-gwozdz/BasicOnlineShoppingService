using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Common.Exceptions;
using BasicOnlineShoppingService.Modules.CartModule.Carts.Events;
using MassTransit;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Requests;

internal class DeleteCartRequest(CartsMongoContext cartsMongoContext, IPublishEndpoint publishEndpoint) : IRequest
{
    public async Task Handle(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<CartEntity>.Filter.Eq(cartEntity => cartEntity.Id, id);
        var cart = await (await cartsMongoContext.Collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        if (cart is null)
        {
            throw new NotFoundException(nameof(CartEntity), $"with id: {id}");
        }
        await cartsMongoContext.Collection.DeleteOneAsync(filter, cancellationToken);
        await publishEndpoint.Publish(new CartDeletedEvent(cart.Id), cancellationToken);
    }
}