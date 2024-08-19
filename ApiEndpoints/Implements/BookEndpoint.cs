using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

            if (books != null)
            {
                foreach (var book in books)
                {
                    book.Authors = service.GetAuthors(book.Id);
                    book.Categories = service.GetCategories(book.Id);
                }

                return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
            }

            return Results.NotFound("No books found.");
        }).WithName("GetAllBooks");

        // Get books by author
        apiGroup.MapGet("/books/author/{authorId}", ([FromServices] IBookService service, long authorId) =>
        {
            var books = service.GetAllByAuthor(authorId);

            if (books != null)
            {
                foreach (var book in books)
                {
                    book.Authors = service.GetAuthors(book.Id);
                    book.Categories = service.GetCategories(book.Id);
                }

                return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
            }

            return Results.NotFound("No books found.");
        }).WithName("GetBooksByAuthor");

        // Get books by category
        apiGroup.MapGet("/books/category/{categoryId}", ([FromServices] IBookService service, long categoryId) =>
        {
            var books = service.GetAllByCategory(categoryId);

            if (books != null)
            {
                foreach (var book in books)
                {
                    book.Authors = service.GetAuthors(book.Id);
                    book.Categories = service.GetCategories(book.Id);
                }

                return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
            }

            return Results.NotFound("No books found.");
        }).WithName("GetBooksByCategory");

        // Get books by title
        apiGroup.MapGet("/books/title/{title}", ([FromServices] IBookService service, string title) =>
        {
            var books = service.GetAllByTitle(title);

            if (books != null)
            {
                foreach (var book in books)
                {
                    book.Authors = service.GetAuthors(book.Id);
                    book.Categories = service.GetCategories(book.Id);
                }

                return books.Count > 0 ? Results.Ok(books) : Results.NotFound("No books found.");
            }

            return Results.NotFound("No books found.");
        }).WithName("GetBooksByTitle");
        
        // Get book by id
        apiGroup.MapGet("/books/{id}", ([FromServices] IBookService service, long id) =>
        {
            var book = service.GetById(id);
            if (book != null)
            {
                book.Authors = service.GetAuthors(book.Id);
                book.Categories = service.GetCategories(book.Id);

                return Results.Ok(book);
            }

            return Results.NotFound("Book not found.");
        }).WithName("GetBookById");

        // Add book
        apiGroup.MapPost("/books",
            [Authorize(Roles = "admin")]
            async (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookService service) =>
            {
                await antiforgery.ValidateRequestAsync(context);

                var form = context.Request.Form;
                Console.WriteLine("Form: " + form["title"]);
                var bookDto = new BookDto
                (
                    form["title"],
                    form["isbn"],
                    form["description"],
                    form["importedDate"].Select(DateTime.Parse).First(),
                    form["quantity"].Select(int.Parse).First(),
                    form["bookImage"]
                );

                bookDto.SetIds(form["authorIds[]"].Select(long.Parse).ToList(),
                    form["categoryIds[]"].Select(long.Parse).ToList());

                var result = service.Add(bookDto);
                result.Authors = service.GetAuthors(result.Id);
                result.Categories = service.GetCategories(result.Id);

                return result != null
                    ? Results.Created($"/books/{result.Id}", result)
                    : Results.BadRequest("Book not added.");
            }).WithName("AddBook");

        // Update book
        apiGroup.MapPut("/books/{id}",
            [Authorize(Roles = "admin")] async (HttpContext context, IAntiforgery antiforgery,
                [FromServices] IBookService service, long id,
                [FromForm] BookDto bookDto) =>
            {
                await antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, bookDto);

                return result != null ? Results.Ok(result) : Results.BadRequest("Book not updated.");
            }).WithName("UpdateBook");

        // Delete book
        apiGroup.MapDelete("/books/{id}",
            [Authorize(Roles = "admin")]
            async (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookService service, long id) =>
            {
                await antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Book deleted.") : Results.BadRequest("Book not deleted.");
            }).WithName("DeleteBook");
    }
}