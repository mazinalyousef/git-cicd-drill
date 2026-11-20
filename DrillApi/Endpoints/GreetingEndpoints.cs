namespace DrillApi.Endpoints;

public static class GreetingEndpoints
{
    public static IEndpointRouteBuilder MapGreetingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/hello/{name}", (string name) =>
        {
            var trimmed = name.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                return Results.BadRequest(new { error = "Name cannot be empty." });
            }

            return Results.Ok(new
            {
                message = $"Hello, {trimmed}!"
            });
        })
        .WithName("GetGreeting");

        return app;
    }
}