namespace BasicOnlineShoppingService.App;

public class Program
{
    protected Program()
    {
    }

    public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                webBuilder
                    .ConfigureAppConfiguration((context, config) =>
                    {
                        config.AddJsonFile("appsettings.json", true, true);
                        config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true, true);
                        config.AddJsonFile("appsettings.User.json", true, true);
                        config.AddEnvironmentVariables();
                    })
                    .UseStartup<Startup>());
}