using MassTransit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Events;

public record ProductDeletedEvent(Guid CorrelationId, Guid ProductId)
{
    public ProductDeletedEvent(Guid productId) : this(NewId.NextGuid(), productId)
    {
    }
}