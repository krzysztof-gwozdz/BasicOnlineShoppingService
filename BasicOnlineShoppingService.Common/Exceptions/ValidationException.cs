namespace BasicOnlineShoppingService.Common.Exceptions;

public class ValidationException(ValidationErrorsSet errors) : BaseException
{
    public override string Title => "validation_error";
    public override string Detail => "An error occurred during validation.";
    public ValidationErrorsSet Errors { get; } = errors;
    
    public ValidationException(ValidationError error) : this(new ValidationErrorsSet(error))
    {
    }

    public ValidationException(string title, string detail) : this(new ValidationErrorsSet(new ValidationError(title, detail)))
    {
    }
}