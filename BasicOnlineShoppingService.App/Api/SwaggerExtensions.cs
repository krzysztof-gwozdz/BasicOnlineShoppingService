namespace BasicOnlineShoppingService.App.Api;

public static class SwaggerExtensions
{
    public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "Basic Online Shopping Service", Version = "v1" });
        });
    }

    public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basic Online Shopping Service"));
        return app;
    }
}