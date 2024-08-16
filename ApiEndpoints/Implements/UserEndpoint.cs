using Library.Dto.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class UserEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all users
        apiGroup.MapGet("/users", ([FromServices] IUserService service) =>
        {
            var users = service.GetAll();
            return users.Count > 0 ? Results.Ok(users) : Results.NotFound("No users found.");
        }).WithName("GetAllUsers");

        // Get Loan by user
        apiGroup.MapGet("/users/{id}/loans", ([FromServices] IUserService service, int id) =>
        {
            var user = service.GetById(id);
            if (user == null)
            {
                return Results.NotFound("User not found.");
            }

            var loans = user.Loans;
            return loans.Count > 0 ? Results.Ok(loans) : Results.NotFound("No loans found.");
        }).WithName("GetLoansByUser");

        // Get user by id
        apiGroup.MapGet("/users/{id}", ([FromServices] IUserService service, int id) =>
        {
            var user = service.GetById(id);
            return user != null ? Results.Ok(user) : Results.NotFound("User not found.");
        }).WithName("GetUserById");

        // Update user
        apiGroup.MapPut("/users/{id}",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IUserService service, int id,
                UserDto userDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var updated = service.Update(id, (User)userDto.ToEntity());
                return updated ? Results.Ok("User updated.") : Results.BadRequest("Failed to update user.");
            }).WithName("UpdateUser");

        // Delete user
        apiGroup.MapDelete("/users/{id}",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IUserService service, int id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var deleted = service.Delete(id);
                return deleted ? Results.Ok("User deleted.") : Results.BadRequest("Failed to delete user.");
            }).WithName("DeleteUser");
    }
}