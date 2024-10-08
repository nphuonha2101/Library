using Library.Dto.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class CategoryEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all categories
        apiGroup.MapGet("/categories", ([FromServices] ICategoryService service) =>
        {
            var categories = service.GetAll();
            return categories != null && categories.Count > 0 ? Results.Ok(categories) : Results.NotFound("No categories found.");
        }).WithName("GetAllCategories");

        // Get category by id
        apiGroup.MapGet("/categories/{id}", ([FromServices] ICategoryService service, long id) =>
        {
            var category = service.GetById(id);
            return category != null ? Results.Ok(category) : Results.NotFound("Category not found.");
        }).WithName("GetCategoryById");

        // Add category
        apiGroup.MapPost("/categories",
            [Authorize(Roles = "admin")](HttpContext context, IAntiforgery antiforgery,
                [FromServices] ICategoryService service,
                [FromForm] CategoryDto categoryDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Add((Category)categoryDto.ToEntity());
                return result != null
                    ? Results.Created($"/categories/{result.Id}", result)
                    : Results.BadRequest("Category not added.");
            }).WithName("AddCategory");

        // Update category
        apiGroup.MapPut("/categories/{id}",
            [Authorize(Roles = "admin")](HttpContext context, IAntiforgery antiforgery,
                [FromServices] ICategoryService service, long id,
                [FromForm] CategoryDto categoryDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, (Category)categoryDto.ToEntity());
                return result != null ? Results.Ok(categoryDto) : Results.BadRequest("Category not updated.");
            }).WithName("UpdateCategory");

        // Delete category
        apiGroup.MapDelete("/categories/{id}",
            [Authorize(Roles = "admin")](HttpContext context, IAntiforgery antiforgery,
                [FromServices] ICategoryService service, long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Category deleted.") : Results.BadRequest("Category not deleted.");
            }).WithName("DeleteCategory");
    }
}