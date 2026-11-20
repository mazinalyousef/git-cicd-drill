using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DrillApi.Tests;

public class GreetingEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GreetingEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetHello_WithValidName_ReturnsOkAndGreetingMessage()
    {
        // Arrange
        var name = "Mazen";

        // Act
        var response = await _client.GetAsync($"/hello/{name}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal("Hello, Mazen!", payload.GetProperty("message").GetString());
    }

    [Fact]
    public async Task GetHello_WithWhitespaceName_ReturnsBadRequest()
    {
        // Act
        var response = await _client.GetAsync("/hello/%20%20%20");

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

}