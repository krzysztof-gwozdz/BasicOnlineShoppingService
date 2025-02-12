namespace BasicOnlineShoppingService.Common.Exceptions;

public class NotFoundException(string type, string criterion) : BaseException
{
    public override string Title => "not_found_error";
    public override string Detail { get; } = $"{type} {criterion} not found.";
}