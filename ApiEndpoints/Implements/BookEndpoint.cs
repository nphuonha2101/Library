using Library.Constants;
using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Exceptions;
using Library.Services.Interfaces;
using Library.Utils.File;
using Library.Utils.Paths;
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
            async (IWebHostEnvironment webHostEnvironment, HttpContext context, IAntiforgery antiforgery,
                [FromServices] IBookService service) =>
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

                var file = context.Request.Form.Files["file-upload"];

                try
                {
                    var host = PathUtils.GetHost(context);
                    bookDto.BookImage = UploadFile.UploadImage(file,
                        PathUtils.GetWebRootPath(webHostEnvironment), WwwRootPath.IMAGES, host);
                }
                catch (NoFileException e)
                {
                    return Results.BadRequest(e.Message);
                }
                catch (InvalidFileException e)
                {
                    return Results.BadRequest(e.Message);
                }

                var result = service.Add(bookDto);
                result.Authors = service.GetAuthors(result.Id);
                result.Categories = service.GetCategories(result.Id);

                return result != null
                    ? Results.Created($"/books/{result.Id}", result)
                    : Results.BadRequest("Book not added.");
            }).WithName("AddBook");

        // Update book
        apiGroup.MapPut("/books/{id}",
            async (IWebHostEnvironment webHostEnvironment, HttpContext context, IAntiforgery antiforgery,
                [FromServices] IBookService service, long id) =>
            {
                await antiforgery.ValidateRequestAsync(context);

                var form = context.Request.Form;

                var title = !string.IsNullOrWhiteSpace(form["title"]) ? form["title"].ToString() : null;
                var isbn = !string.IsNullOrWhiteSpace(form["isbn"]) ? form["isbn"].ToString() : null;
                var description = !string.IsNullOrWhiteSpace(form["description"])
                    ? form["description"].ToString()
                    : null;
                var importedDate = !string.IsNullOrWhiteSpace(form["importedDate"])
                    ? DateTime.Parse(form["importedDate"])
                    : (DateTime?)null;
                var quantity = !string.IsNullOrWhiteSpace(form["quantity"]) ? int.Parse(form["quantity"]) : (int?)null;
                var bookImage = !string.IsNullOrWhiteSpace(form["bookImage"]) ? form["bookImage"].ToString() : null;

                var bookDto = new BookDto(title, isbn, description, importedDate ?? default, quantity ?? default,
                    bookImage);

                if (!string.IsNullOrWhiteSpace(form["authorIds[]"]))
                {
                    bookDto.SetIds(form["authorIds[]"].Select(long.Parse).ToList(), null);
                }

                if (!string.IsNullOrWhiteSpace(form["categoryIds[]"]))
                {
                    bookDto.SetIds(null, form["categoryIds[]"].Select(long.Parse).ToList());
                }

                var file = context.Request.Form.Files["file-upload"];

                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var host = PathUtils.GetHost(context);
                        bookDto.BookImage = UploadFile.UploadImage(file,
                            PathUtils.GetWebRootPath(webHostEnvironment), WwwRootPath.IMAGES, host);
                    }
                }
                catch (NoFileException e)
                {
                    return Results.BadRequest(e.Message);
                }
                catch (InvalidFileException e)
                {
                    return Results.BadRequest(e.Message);
                }

                var result = service.Update(id, bookDto);

                return result != null ? Results.Ok(result) : Results.BadRequest("Book not updated.");
            }).WithName("UpdateBook");

        // Delete book
        apiGroup.MapDelete("/books/{id}",
            async (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookService service, long id) =>
            {
                await antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Book deleted.") : Results.BadRequest("Book not deleted.");
            }).WithName("DeleteBook");
    }
}