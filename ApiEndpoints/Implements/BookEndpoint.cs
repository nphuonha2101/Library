using Library.Models;
using Library.Services.Implements;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class BookEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
    {
        // Get all books
        apiGroup.MapGet("/books", async (BookService service) =>
        {
            var books = await Task.FromResult(service.GetAll());
            return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
        }).WithName("GetAllBooks");

        // Get book by id
        apiGroup.MapGet("/books/{id}", async (BookService service, int id) =>
        {
            var book = await Task.FromResult(service.GetById(id));
            return book != null ? Results.Ok(book) : Results.NotFound("Book not found.");
        }).WithName("GetBookById");
        
        // Add book
        apiGroup.MapPost("/books", async (BookService service, [FromForm] Book book) =>
        {
            var result = await Task.FromResult(service.Add(book));
            return result != null ? Results.Created($"/books/{book.Id}", book) : Results.BadRequest("Book not added.");
        }).WithName("AddBook");
        
        // Update book
        apiGroup.MapPut("/books/{id}", async (BookService service, int id, [FromForm] Book book) =>
        {
            var result = await Task.FromResult(service.Update(id, book));
            return result ? Results.Ok(book) : Results.BadRequest("Book not updated.");
        }).WithName("UpdateBook");
        
        // Delete book
        apiGroup.MapDelete("/books/{id}", async (BookService service, int id) =>
        {
            var result = await Task.FromResult(service.Delete(id));
            return result ? Results.Ok("Book deleted.") : Results.BadRequest("Book not deleted.");
        }).WithName("DeleteBook");
    }
}