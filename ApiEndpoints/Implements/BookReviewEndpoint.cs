using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class BookReviewEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all book reviews
        apiGroup.MapGet("/book-reviews", ([FromServices] IBookReviewService bookReviewService, 
            [FromServices] IUserService userService, [FromServices] IBookService bookService) =>
        {
            var bookReviews = bookReviewService.GetAll();
            if (bookReviews != null && bookReviews.Count > 0)
            {
                foreach (var bookReview in bookReviews)
                {
                    bookReview.Book = bookService.GetById(bookReview.BookId);
                    bookReview.User = userService.GetById(bookReview.UserId);
                }

                return Results.Ok(bookReviews);
            }

            return Results.NotFound("No book reviews found.");
        });

        // Get book review by id
        apiGroup.MapGet("/book-reviews/{id}", ([FromServices] IBookReviewService bookReviewService,
            [FromServices] IUserService userService, [FromServices] IBookService bookService
            , long id) =>
        {
            var bookReview = bookReviewService.GetById(id);
            
            if (bookReview != null)
            {
                bookReview.Book = bookService.GetById(bookReview.BookId);
                bookReview.User = userService.GetById(bookReview.UserId);
                
                return Results.Ok(bookReview);
            }
            
            return Results.NotFound("Book review not found.");
        });

        // Get book reviews by book id
        apiGroup.MapGet("/book-reviews/book/{bookId}",
            ([FromServices] IBookReviewService bookReviewService,
            [FromServices] IUserService userService, [FromServices] IBookService bookService
            , long bookId) =>
            {
                var bookReviews = bookReviewService.GetByBookId(bookId);
                if (bookReviews != null && bookReviews.Count > 0)
                {
                    foreach (var bookReview in bookReviews)
                    {
                        bookReview.Book = bookService.GetById(bookReview.BookId);
                        bookReview.User = userService.GetById(bookReview.UserId);
                    }

                    return Results.Ok(bookReviews);
                }
                
                return Results.NotFound("No book reviews found.");
            });

        // Get book reviews by user id
        apiGroup.MapGet("/book-reviews/user/{userId}",
            ([FromServices] IBookReviewService bookReviewService,
                [FromServices] IUserService userService, [FromServices] IBookService bookService
                , long userId) =>
            {
                var bookReviews = bookReviewService.GetByUserId(userId);
                if (bookReviews != null && bookReviews.Count > 0)
                {
                    foreach (var bookReview in bookReviews)
                    {
                        bookReview.Book = bookService.GetById(bookReview.BookId);
                        bookReview.User = userService.GetById(bookReview.UserId);
                    }

                    return Results.Ok(bookReviews);
                }
                return Results.BadRequest("No book reviews found.");
            });

        // Add book review
        apiGroup.MapPost("/book-reviews",
          [Authorize]  (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookReviewService service,
                [FromServices] IBookService bookService, [FromServices] IUserService userService) =>
            {
                antiforgery.ValidateRequestAsync(context);

                var form = context.Request.Form;
                
                var createdAt = form.ContainsKey("createdAt") && DateTime.TryParse(form["createdAt"], out var parsedDate)
                    ? parsedDate
                    : DateTime.Now;
                
                var bookReviewDto = new BookReviewDto(
                    long.Parse(form["bookId"]),
                    long.Parse(form["userId"]),
                    form["title"],
                    form["review"],
                    int.Parse(form["rating"]),
                    createdAt
                );

                var result = service.Add((BookReview)bookReviewDto.ToEntity());
                if (result != null)
                {
                    result.Book = bookService.GetById(result.BookId);
                    result.User = userService.GetById(result.UserId);
                    return Results.Created($"/book-reviews/{result.Id}", result);
                }

                return Results.BadRequest("Book review not added.");
            }).WithName("AddBookReview");

        // Update book review
        apiGroup.MapPut("/book-reviews/{id}",
          [Authorize]  (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookReviewService bookReviewService,
                [FromServices] IBookService bookService, [FromServices] IUserService userService, long id) =>
            {
                antiforgery.ValidateRequestAsync(context);

                var form = context.Request.Form;
                
                var createdAt = form.ContainsKey("createdAt") && DateTime.TryParse(form["createdAt"], out var parsedDate)
                    ? parsedDate
                    : DateTime.Now;
                
                var bookReviewDto = new BookReviewDto(
                    long.Parse(form["bookId"]),
                    long.Parse(form["userId"]),
                    form["title"],
                    form["review"],
                    int.Parse(form["rating"]),
                    createdAt
                );

                var result = bookReviewService.Update(id, (BookReview)bookReviewDto.ToEntity());
                if (result != null)
                {
                    result.Book = bookService.GetById(result.BookId);
                    result.User = userService.GetById(result.UserId);
                    return Results.Ok(result);
                }

                return Results.BadRequest("Book review not updated.");
            });

        // Delete book review
        apiGroup.MapDelete("/book-reviews/{id}",
           [Authorize] (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookReviewService bookReviewService,
                long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = bookReviewService.Delete(id);
                return result ? Results.Ok("Book review deleted.") : Results.BadRequest("Book review not deleted.");
            });
    }
}