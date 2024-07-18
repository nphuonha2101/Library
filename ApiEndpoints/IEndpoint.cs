namespace Library.ApiEndpoints;

/**
 * Interface for API endpoints
 * All endpoints should implement this interfaces and define their endpoints in method DefineEndpoints
 */
public interface IEndpoint
{
    void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup);
}