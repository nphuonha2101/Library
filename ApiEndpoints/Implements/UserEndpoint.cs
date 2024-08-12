using Library.Services.Interfaces;
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
        
        // Get user by id
        apiGroup.MapGet("/users/{id}", ([FromServices] IUserService service, int id) =>
        {
            var user = service.GetById(id);
            return user != null ? Results.Ok(user) : Results.NotFound("User not found.");
        }).WithName("GetUserById");
        
        // Add user
        apiGroup.MapPost("/users", ([FromServices] IUserService service, User user) =>
        {
            var newUser = service.Add(user);
            return newUser != null ? Results.Created($"/users/{newUser.Id}", newUser) : Results.BadRequest("Failed to add user.");
        }).WithName("AddUser");
        
        // Update user
        apiGroup.MapPut("/users/{id}", ([FromServices] IUserService service, int id, User user) =>
        {
            var updated = service.Update(id, user);
            return updated ? Results.Ok("User updated.") : Results.BadRequest("Failed to update user.");
        }).WithName("UpdateUser");
        
        // Delete user
        apiGroup.MapDelete("/users/{id}", ([FromServices] IUserService service, int id) =>
        {
            var deleted = service.Delete(id);
            return deleted ? Results.Ok("User deleted.") : Results.BadRequest("Failed to delete user.");
        }).WithName("DeleteUser");
    }
}