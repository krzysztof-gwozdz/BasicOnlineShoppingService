namespace BasicOnlineShoppingService.Common.Exceptions;

public class ValidationErrorsSet(params ValidationError[] validationErrors) : HashSet<ValidationError>(validationErrors)
{
    public bool HasError => this.Any();

    public void Add(string title, string detail) => Add(new ValidationError(title, detail));
}