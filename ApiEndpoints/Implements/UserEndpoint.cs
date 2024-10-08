using Library.Dto.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class UserEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all users
        apiGroup.MapGet("/users",  ([FromServices] IUserService service) =>
        {
            var users = service.GetAll();
            return users != null && users.Count > 0 ? Results.Ok(users) : Results.NotFound("No users found.");
        }).WithName("GetAllUsers");
        
        // Get user by id
        apiGroup.MapGet("/users/{id}",  ([FromServices] IUserService service, long id) =>
        {
            var user = service.GetById(id);
            return user != null ? Results.Ok(user) : Results.NotFound("User not found.");
        }).WithName("GetUserById");

        // Update user
        apiGroup.MapPut("/users/{id}",
            [Authorize]   (HttpContext context, IAntiforgery antiforgery, [FromServices] IUserService service, long id,
                UserDto userDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var updated = service.Update(id, (User)userDto.ToEntity());
                return updated != null ? Results.Ok("User updated.") : Results.BadRequest("Failed to update user.");
            }).WithName("UpdateUser");

        // Delete user
        apiGroup.MapDelete("/users/{id}",
            [Authorize(Roles = "admin")]  (HttpContext context, IAntiforgery antiforgery, [FromServices] IUserService service, long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var deleted = service.Delete(id);
                return deleted ? Results.Ok("User deleted.") : Results.BadRequest("Failed to delete user.");
            }).WithName("DeleteUser");
    }
}