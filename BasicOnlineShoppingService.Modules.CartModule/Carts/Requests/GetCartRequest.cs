using BasicOnlineShoppingService.Common;
using BasicOnlineShoppingService.Common.Exceptions;
using MongoDB.Driver;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Requests;

internal class GetCartRequest(CartsMongoContext cartsMongoContext) : IRequest
{
    public async Task<Cart> Handle(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<CartEntity>.Filter.Eq(cartEntity => cartEntity.Id, id);
        var cart = await (await cartsMongoContext.Collection.FindAsync(filter, cancellationToken: cancellationToken)).FirstOrDefaultAsync(cancellationToken);
        if (cart is null)
        {
            throw new NotFoundException(nameof(CartEntity), $"with id: {id}");
        }
        return new Cart(cart.Id, cart.Email, cart.Products.Select(product => new Cart.Product(product.Id, product.Name, product.Price, product.Quantity)).ToArray());
    }
}