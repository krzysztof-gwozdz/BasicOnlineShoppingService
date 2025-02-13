using MassTransit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Events;

public record ProductAddedEvent(Guid CorrelationId, Guid ProductId)
{
    public ProductAddedEvent(Guid productId) : this(NewId.NextGuid(), productId)
    {
    }
}