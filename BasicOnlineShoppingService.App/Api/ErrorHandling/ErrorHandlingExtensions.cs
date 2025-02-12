using BasicOnlineShoppingService.App.Api.ErrorHandling.ProblemDetails;
using BasicOnlineShoppingService.Common.Exceptions;
using Hellang.Middleware.ProblemDetails;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

namespace BasicOnlineShoppingService.App.Api.ErrorHandling;

public static class ErrorHandlingExtensions
{
    public static IServiceCollection AddErrorHandling(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddProblemDetails(configure =>
        {
            AddHandlers(configure);
            configure.IncludeExceptionDetails = (_, _) => environment.IsEnvironment("Development") || environment.IsEnvironment("Tests");
            configure.ContentTypes.Add("problem+json");
        });
        return services;
    }

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
    {
        app.UseProblemDetails();
        return app;
    }

    private static void AddHandlers(ProblemDetailsOptions configure)
    {
        configure.Map<InternalServerErrorException>((httpContext, exception) => new InternalServerErrorProblemDetails(httpContext, exception));
        configure.Map<ValidationException>((httpContext, exception) => new ValidationProblemDetails(httpContext, exception));
        configure.Map<NotFoundException>((httpContext, exception) => new NotFoundProblemDetails(httpContext, exception));
        configure.Map<InvalidSortFieldException>((httpContext, exception) => new InvalidSortFieldProblemDetails(httpContext, exception));
    }
}