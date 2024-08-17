using Library.Dto.Implements;
using Library.Services.Interfaces;
using Library.Utils.Securities;
using Library.Utils.Securities.Jwt;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class Authentication : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        application.MapPost("/login",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IConfiguration configuration,
                [FromServices] IUserService userService,
                [FromForm] string usernameOrEmail, [FromForm] string password) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var user = userService.Login(usernameOrEmail, password);
                var token = user != null ? new BearerToken(configuration).GenerateJwtToken(user) : null;

                if (user != null && token != null) return Results.Ok(new { token, user });

                return Results.Unauthorized();
            }).WithName("Login");

        application.MapPost("/logout", (HttpContext context, ITokenInvalid tokenInvalid) =>
        {
            var token = context.Request.Headers["Authorization"].ToString();
            tokenInvalid.Add(token);
            return Results.Ok("Logout successfully");
        }).WithName("Logout");

        application.MapPost("/register",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IUserService userService,
                [FromForm] UserDto userDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = userService.Register(userDto);
                return result != null
                    ? Results.Created($"/users/{result.Id}", result)
                    : Results.BadRequest("User not registered.");
            }).WithName("Register");
    }
}