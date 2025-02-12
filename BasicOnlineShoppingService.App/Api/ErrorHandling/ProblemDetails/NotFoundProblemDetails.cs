using BasicOnlineShoppingService.Common.Exceptions;

namespace BasicOnlineShoppingService.App.Api.ErrorHandling.ProblemDetails;

public class NotFoundProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public NotFoundProblemDetails(HttpContext httpContext, NotFoundException exception)
    {
        Status = StatusCodes.Status404NotFound;
        Type = $"https://httpstatuses.com/{StatusCodes.Status404NotFound}";
        Title = exception.Title;
        Detail = exception.Detail;
        Instance = httpContext.Request.Path;
    }
}