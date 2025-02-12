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
                    .ConfigureAppConfiguration(configure => configure.AddJsonFile("appsettings.User.json", true))
                    .UseStartup<Startup>());
}