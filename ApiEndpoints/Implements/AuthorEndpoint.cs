using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class AuthorEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
    {
        // Get all authors
        apiGroup.MapGet("/authors", (IAuthorService service) =>
        {
            var authors = service.GetAll();
            return authors.Count > 0 ? Results.Ok(authors) : Results.NotFound("No authors found.");
        }).WithName("GetAllAuthors");

        // Get author by id
        apiGroup.MapGet("/authors/{id}", (IAuthorService service, int id) =>
        {
            var author = service.GetById(id);
            return author != null ? Results.Ok(author) : Results.NotFound("Author not found.");
        }).WithName("GetAuthorById");

        // Add author
        apiGroup.MapPost("/authors",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IAuthorService service,
                [FromForm] AuthorDto authorDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Add((Author)authorDto.ToEntity());
                return result != null
                    ? Results.Created($"/authors/{result.Id}", result)
                    : Results.BadRequest("Author not added.");
            }).WithName("AddAuthor");

        // Update author
        apiGroup.MapPut("/authors/{id}",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IAuthorService service, int id,
                [FromForm] AuthorDto authorDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, (Author)authorDto.ToEntity());
                return result ? Results.Ok(result) : Results.BadRequest("Author not updated.");
            }).WithName("UpdateAuthor");

        // Delete author
        apiGroup.MapDelete("/authors/{id}",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IAuthorService service, int id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Author deleted.") : Results.BadRequest("Author not deleted.");
            }).WithName("DeleteAuthor");
    }
}