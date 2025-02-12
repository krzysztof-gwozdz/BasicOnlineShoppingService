namespace BasicOnlineShoppingService.Common.Exceptions;

public class InvalidSortFieldException(string objectType, string sortField) : BaseException
{
    public override string Title => "invalid_sort_field";
    public override string Detail { get; } = $"{objectType} cannot be sorted by {sortField}.";
}