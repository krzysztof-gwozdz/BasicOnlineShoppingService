namespace BasicOnlineShoppingService.Modules.StoreModule.Products.Dtos;

public record AddProductDto(string Name, string Description, decimal Price, string Category);