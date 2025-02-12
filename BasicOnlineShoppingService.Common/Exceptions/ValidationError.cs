namespace BasicOnlineShoppingService.Common.Exceptions;

public class ValidationError(string title, string detail)
{
    public string Title { get; } = title;
    public string Detail { get; } = detail;

    public override string ToString() => $"{Title}: {Detail}";
}