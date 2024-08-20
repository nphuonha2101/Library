using Library.Dto.Implements;
using Library.Services.Interfaces;
using Library.Utils.Securities;
using Library.Utils.Securities.Jwt;
using Library.Utils.Validations;
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
                var isEmail = new EmailValidation().IsValid(usernameOrEmail);
                var isUsername = new UsernameValidation().IsValid(usernameOrEmail);
                var isPasswordValid = new PasswordValidation().IsValid(password);

                if (!isEmail && !isUsername)
                {
                    return Results.BadRequest("Username or email is invalid.");
                }

                if (!isPasswordValid)
                {
                    return Results.BadRequest("Password is invalid.");
                }
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
                [FromForm] string fullName, [FromForm] string username, [FromForm] string email,
                [FromForm] string address, [FromForm] string password, [FromForm] DateTime dob) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var isEmailValid = new EmailValidation().IsValid(email);
                var isUsernameValid = new UsernameValidation().IsValid(username);
                var isPasswordValid = new PasswordValidation().IsValid(password);
                if (!isEmailValid)
                {
                    return Results.BadRequest("Email is invalid.");
                }

                if (!isUsernameValid)
                {
                    return Results.BadRequest("Username is invalid.");
                }

                if (!isPasswordValid)
                {
                    return Results.BadRequest("Password is invalid.");
                }

                if (string.IsNullOrWhiteSpace(fullName))
                {
                    return Results.BadRequest("Full Name is required.");
                }
                var userDto = new UserDto(fullName, username, email, address, password, dob, false);
                var result = userService.Register(userDto);
                return result != null
                    ? Results.Created($"/users/{result.Id}", result)
                    : Results.BadRequest("User not registered.");
            }).WithName("Register");
    }
}