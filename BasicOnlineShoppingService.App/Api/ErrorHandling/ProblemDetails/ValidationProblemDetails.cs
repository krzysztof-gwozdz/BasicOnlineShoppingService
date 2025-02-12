using BasicOnlineShoppingService.Common.Exceptions;

namespace BasicOnlineShoppingService.App.Api.ErrorHandling.ProblemDetails;

public class ValidationProblemDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public ValidationProblemDetails(HttpContext httpContext, ValidationException exception)
    {
        Status = StatusCodes.Status422UnprocessableEntity;
        Type = $"https://httpstatuses.com/{StatusCodes.Status422UnprocessableEntity}";
        Title = exception.Title;
        Detail = exception.Detail;
        Instance = httpContext.Request.Path;
        Extensions.Add("errors", exception.Errors);
    }
}