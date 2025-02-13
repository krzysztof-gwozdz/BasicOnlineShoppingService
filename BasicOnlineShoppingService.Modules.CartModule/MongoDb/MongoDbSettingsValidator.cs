using Microsoft.Extensions.Options;

namespace BasicOnlineShoppingService.Modules.CartModule.MongoDb;

public class MongoDbOptionsValidator : IValidateOptions<MongoDbOptions>
{
    public ValidateOptionsResult Validate(string? name, MongoDbOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.ConnectionString))
        {
            return ValidateOptionsResult.Fail($"{nameof(MongoDbOptions.ConnectionString)} cannot be null or empty");
        }
        if (string.IsNullOrWhiteSpace(options.DatabaseName))
        {
            return ValidateOptionsResult.Fail($"{nameof(MongoDbOptions.DatabaseName)} cannot be null or empty");
        }
        return ValidateOptionsResult.Success;
    }
}