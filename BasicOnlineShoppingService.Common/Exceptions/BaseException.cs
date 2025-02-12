namespace BasicOnlineShoppingService.Common.Exceptions;

public abstract class BaseException : Exception
{
    public abstract string Title { get; }
    public abstract string Detail { get; }
    public override string Message => $"{Title}: {Detail}";

    protected BaseException()
    {
    }

    protected BaseException(string message, Exception? innerException) : base(message, innerException)
    {
    }
}