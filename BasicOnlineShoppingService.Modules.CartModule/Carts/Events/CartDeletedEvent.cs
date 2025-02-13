using MassTransit;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Events;

public class CartDeletedEvent(Guid CorrelationId, Guid CartId)
{
    public CartDeletedEvent(Guid cartId) : this(NewId.NextGuid(), cartId)
    {
    }
}