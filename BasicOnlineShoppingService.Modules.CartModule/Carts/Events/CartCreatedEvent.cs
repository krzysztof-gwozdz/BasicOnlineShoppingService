using MassTransit;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Events;

public class CartCreatedEvent(Guid CorrelationId, Guid CartId)
{
    public CartCreatedEvent(Guid cartId) : this(NewId.NextGuid(), cartId)
    {
    }
}