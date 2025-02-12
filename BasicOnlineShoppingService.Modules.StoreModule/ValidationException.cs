namespace BasicOnlineShoppingService.Modules.StoreModule;

public class ValidationException(string propertyName, string message) : Exception(message)
{
    public string PropertyName { get; init; } = propertyName;
}