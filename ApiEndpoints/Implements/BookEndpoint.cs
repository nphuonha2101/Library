using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class BookEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
    {
        // Get all books
        apiGroup.MapGet("/books", ([FromServices] IBookService service) =>
        {
            var books = service.GetAll();

            foreach (var book in books)
            {
                book.Authors = service.GetAuthors(book.Id);
                book.Categories = service.GetCategories(book.Id);
            }

            return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
        }).WithName("GetAllBooks");

        // Get books by author
        apiGroup.MapGet("/books/author/{id}", ([FromServices] IBookService service, int id) =>
        {
            var books = service.GetAllByAuthor(id);

            foreach (var book in books)
            {
                book.Authors = service.GetAuthors(book.Id);
                book.Categories = service.GetCategories(book.Id);
            }

            return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
        }).WithName("GetBooksByAuthor");

        // Get books by category
        apiGroup.MapGet("/books/category/{id}", ([FromServices] IBookService service, int id) =>
        {
            var books = service.GetAllByCategory(id);

            foreach (var book in books)
            {
                book.Authors = service.GetAuthors(book.Id);
                book.Categories = service.GetCategories(book.Id);
            }

            return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
        }).WithName("GetBooksByCategory");

        // Get book by id
        apiGroup.MapGet("/books/{id}", ([FromServices] IBookService service, int id) =>
        {
            var book = service.GetById(id);
            book.Authors = service.GetAuthors(book.Id);
            book.Categories = service.GetCategories(book.Id);

            return book != null ? Results.Ok(book) : Results.NotFound("Book not found.");
        }).WithName("GetBookById");

        // Add book
        apiGroup.MapPost("/books",
            async (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookService service,
                [FromForm] BookDto bookDto) =>
            {
                await antiforgery.ValidateRequestAsync(context);
                var result = service.Add(bookDto);
                result.Authors = service.GetAuthors(result.Id);
                result.Categories = service.GetCategories(result.Id);

                return result != null
                    ? Results.Created($"/books/{result.Id}", result)
                    : Results.BadRequest("Book not added.");
            }).WithName("AddBook");

        // Update book
        apiGroup.MapPut("/books/{id}",
            async (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookService service, int id,
                [FromForm] BookDto bookDto) =>
            {
                await antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, (Book)bookDto.ToEntity());

                return result ? Results.Ok(result) : Results.BadRequest("Book not updated.");
            }).WithName("UpdateBook");

        // Delete book
        apiGroup.MapDelete("/books/{id}",
            async (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookService service, int id) =>
            {
                await antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Book deleted.") : Results.BadRequest("Book not deleted.");
            }).WithName("DeleteBook");
    }
}