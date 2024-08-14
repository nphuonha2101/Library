namespace Library.ApiEndpoints.Implements;

public class Authentication : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        application.MapPost("/login", (string usernameOrEmail, string password) =>
        {
            
        }).WithName("Login");
    }
}