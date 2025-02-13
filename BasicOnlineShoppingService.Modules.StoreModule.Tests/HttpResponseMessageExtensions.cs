using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shouldly;

namespace BasicOnlineShoppingService.Modules.StoreModule.Tests;

public static class HttpResponseMessageExtensions
{
    public static async Task<string> GetContent(this HttpResponseMessage response) =>
        await response.Content.ReadAsStringAsync();

    public static async Task<TContent?> GetContent<TContent>(this HttpResponseMessage response) =>
        JsonConvert.DeserializeObject<TContent>(await response.Content.ReadAsStringAsync());

    public static void AssertOk(this HttpResponseMessage response)
    {
        response.ShouldNotBeNull();
        AssertResponseHasHttpStatusCode(response, HttpStatusCode.OK);
    }

    public static void AssertCreated(this HttpResponseMessage response)
    {
        response.ShouldNotBeNull();
        AssertResponseHasHttpStatusCode(response, HttpStatusCode.Created);
    }

    public static void AssertNoContent(this HttpResponseMessage response)
    {
        response.ShouldNotBeNull();
        AssertResponseHasHttpStatusCode(response, HttpStatusCode.NoContent);
    }

    public static async Task AssertNotModified(this HttpResponseMessage response)
    {
        response.ShouldNotBeNull();
        AssertResponseHasHttpStatusCode(response, HttpStatusCode.NotModified);
        var content = await response.GetContent();
        content.ShouldBeNullOrEmpty();
    }

    public static async Task AssertNotFound(this HttpResponseMessage response, string details, string url)
    {
        response.ShouldNotBeNull();
        AssertResponseHasHttpStatusCode(response, HttpStatusCode.NotFound);
        await AssertResponseContainsNotFoundProblemDetails(response, details, url);
    }
    
    public static async Task AssertUnprocessableEntity(this HttpResponseMessage response, string url)
    {
        response.ShouldNotBeNull();
        AssertResponseHasHttpStatusCode(response, HttpStatusCode.UnprocessableEntity);
        await AssertResponseContainsBadRequestProblemDetails(response, "validation_error", "An error occurred during validation.", url);
    }

    private static void AssertResponseHasHttpStatusCode(this HttpResponseMessage response, HttpStatusCode httpStatusCode)
    {
        response.StatusCode.ShouldBe(httpStatusCode);
    }

    private static async Task AssertResponseContainsBadRequestProblemDetails(this HttpResponseMessage response, string title, string details, string url)
    {
        var problemDetails = await response.GetContent<ProblemDetails>();
        problemDetails.ShouldNotBeNull();
        problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
        problemDetails.Type = $"https://httpstatuses.com/{StatusCodes.Status422UnprocessableEntity}";
        problemDetails.Title.ShouldBe(title);
        problemDetails.Detail.ShouldBe(details);
        problemDetails.Instance.ShouldBe(url);
    }
    private static async Task AssertResponseContainsNotFoundProblemDetails(this HttpResponseMessage response, string details, string url)
    {
        var problemDetails = await GetContent<ProblemDetails>(response);
        problemDetails.ShouldNotBeNull();
        problemDetails.Status.ShouldBe(StatusCodes.Status404NotFound);
        problemDetails.Type.ShouldBe($"https://httpstatuses.com/{StatusCodes.Status404NotFound}");
        problemDetails.Title.ShouldBe("not_found_error");
        problemDetails.Detail.ShouldBe(details);
        problemDetails.Instance.ShouldBe(url);
    }
}