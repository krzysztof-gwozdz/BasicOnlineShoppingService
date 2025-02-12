using BasicOnlineShoppingService.Common.Exceptions;

namespace BasicOnlineShoppingService.App.Api.ErrorHandling.ProblemDetails;

public class InternalServerErrorProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public InternalServerErrorProblemDetails(HttpContext httpContext, InternalServerErrorException exception)
    {
        Status = StatusCodes.Status500InternalServerError;
        Type = $"https://httpstatuses.com/{StatusCodes.Status500InternalServerError}";
        Title = exception.Title;
        Detail = exception.Detail;
        Instance = httpContext.Request.Path;
    }
}