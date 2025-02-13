using System.Diagnostics;
using System.Text;
using MassTransit.Testing;
using Newtonsoft.Json;
using Xunit;

namespace BasicOnlineShoppingService.Modules.StoreModule.Tests;

public class IntegrationTestBase: IClassFixture<TestWebApplicationFactory>
{
    protected IServiceProvider Services { get; }
    protected HttpClient HttpClient { get; }
    protected ITestHarness TestHarness { get; }
    protected CancellationToken CancellationToken { get; }

    protected IntegrationTestBase(TestWebApplicationFactory factory)
    {
        Services = factory.Services;
        HttpClient = factory.CreateClient();
        TestHarness = factory.Services.GetTestHarness();
        CancellationToken = (Debugger.IsAttached ? new CancellationTokenSource() : new CancellationTokenSource(TimeSpan.FromSeconds(30))).Token;
    }

    protected Task<HttpResponseMessage> Get(string uri) =>
        HttpClient.GetAsync(uri, CancellationToken);

    protected Task<HttpResponseMessage> Post(string uri, object content) =>
        HttpClient.PostAsync(uri, SerializeToContent(content), CancellationToken);

    protected Task<HttpResponseMessage> Put(string uri, object content) =>
        HttpClient.PutAsync(uri, SerializeToContent(content), CancellationToken);

    protected Task<HttpResponseMessage> Patch(string uri, object content) =>
        HttpClient.PatchAsync(uri, SerializeToContent(content), CancellationToken);

    protected Task<HttpResponseMessage> Delete(string uri) =>
        HttpClient.DeleteAsync(uri, CancellationToken);

    private static StringContent SerializeToContent(object value) =>
        new(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
}