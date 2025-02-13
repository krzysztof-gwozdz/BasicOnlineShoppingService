using BasicOnlineShoppingService.Modules.StoreModule.Products.Events;
using MassTransit;

namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Consumers;

public class ProductDeletedConsumer : IConsumer<ProductDeletedEvent>
{
    public Task Consume(ConsumeContext<ProductDeletedEvent> context)
    {
        // todo - delete product from all carts
        return Task.CompletedTask;
    }
}