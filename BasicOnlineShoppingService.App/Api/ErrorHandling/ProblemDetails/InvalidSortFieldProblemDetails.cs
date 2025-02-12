using BasicOnlineShoppingService.Common.Exceptions;

namespace BasicOnlineShoppingService.App.Api.ErrorHandling.ProblemDetails;

public class InvalidSortFieldProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public InvalidSortFieldProblemDetails(HttpContext httpContext, InvalidSortFieldException exception)
    {
        Status = StatusCodes.Status400BadRequest;
        Type = $"https://httpstatuses.com/{StatusCodes.Status400BadRequest}";
        Title = exception.Title;
        Detail = exception.Detail;
        Instance = httpContext.Request.Path;
    }
}