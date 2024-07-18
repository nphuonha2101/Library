namespace Library.ApiEndpoints;

public interface IEndpoint
{
    void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup);
}