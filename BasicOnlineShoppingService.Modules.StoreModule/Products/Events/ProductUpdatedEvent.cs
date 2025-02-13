using MassTransit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Events;

public record ProductUpdatedEvent(Guid CorrelationId, Guid ProductId)
{
    public ProductUpdatedEvent(Guid productId) : this(NewId.NextGuid(), productId)
    {
    }
}