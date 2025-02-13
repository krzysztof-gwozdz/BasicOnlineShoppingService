namespace BasicOnlineShoppingService.Modules.CartModule.Carts.Dtos;

public record CreateCartDto(string Email);

public record AddProductDto(Guid ProductId, int Quantity);