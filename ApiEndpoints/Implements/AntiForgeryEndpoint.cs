using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Logging;

namespace Library.ApiEndpoints.Implements;

public class AntiForgeryEndpoint : IEndpoint
{
    private readonly ILogger<AntiForgeryEndpoint> _logger;

    public AntiForgeryEndpoint(ILogger<AntiForgeryEndpoint> logger)
    {
        _logger = logger;
    }

    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        apiGroup.MapGet("/antiforgery-token", (IAntiforgery antiForgery, HttpContext context) =>
        {
            var tokens = antiForgery.GetAndStoreTokens(context);

            if (tokens.CookieToken == null || tokens.RequestToken == null)
            {
                _logger.LogError("Failed to generate antiforgery tokens. HttpContext: {HttpContext}", context);
                return Results.NotFound("Anti-forgery tokens not found.");
            }

            context.Response.Cookies.Append("XSRF-TOKEN", tokens.CookieToken, new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddMinutes(30)
            });
            return Results.Ok(new
            {
                requestToken = tokens.RequestToken, headerName = tokens.HeaderName
            });
        }).WithName("GetAntiForgeryToken");
    }
}