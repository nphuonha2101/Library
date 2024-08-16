using Microsoft.AspNetCore.Antiforgery;

namespace Library.ApiEndpoints.Implements;

public class AntiForgeryEndpoint: IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        apiGroup.MapGet("/antiforgery-token", (IAntiforgery antiForgery, HttpContext context) =>
        {
            var tokens = antiForgery.GetAndStoreTokens(context);
            context.Response.Cookies.Append("XSRF-TOKEN", tokens.RequestToken!, new CookieOptions { HttpOnly = false });
            return Results.Ok(new { requestToken = tokens.RequestToken, cookieToken = tokens.CookieToken, headerName = tokens.HeaderName });
        });
    }
}