namespace BasicOnlineShoppingService.Common.Exceptions;

public class InternalServerErrorException(string info, Exception? innerException = null) : BaseException(info, innerException)
{
    public override string Title => "internal_server_error";
    public override string Detail { get; } = $"Internal server error occurred. More info: {info}";
}