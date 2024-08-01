
using Library.Entities.Implements;
using Library.Services.Interfaces;
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
        apiGroup.MapPost("/authors", (IAuthorService service, [FromForm] Author author) =>
        {
            var result = service.Add(author);
            return result != null ? Results.Created($"/authors/{author.Id}", author) : Results.BadRequest("Author not added.");
        }).WithName("AddAuthor");

        // Update author
        apiGroup.MapPut("/authors/{id}", (IAuthorService service, int id, [FromForm] Author author) =>
        {
            var result = service.Update(id, author);
            return result ? Results.Ok(author) : Results.BadRequest("Author not updated.");
        }).WithName("UpdateAuthor");

        // Delete author
        apiGroup.MapDelete("/authors/{id}", (IAuthorService service, int id) =>
        {
            var result = service.Delete(id);
            return result ? Results.Ok("Author deleted.") : Results.BadRequest("Author not deleted.");
        }).WithName("DeleteAuthor");
    }
}