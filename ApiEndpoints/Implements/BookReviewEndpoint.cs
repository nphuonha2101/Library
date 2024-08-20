using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class BookReviewEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all book reviews
        apiGroup.MapGet("/book-reviews", ([FromServices] IBookReviewService bookReviewService) =>
        {
            var bookReviews = bookReviewService.GetAll();
            return bookReviews != null && bookReviews.Count > 0
                ? Results.Ok(bookReviews)
                : Results.NotFound("No book reviews found.");
        });

        // Get book review by id
        apiGroup.MapGet("/book-reviews/{id}", ([FromServices] IBookReviewService bookReviewService, int id) =>
        {
            var bookReview = bookReviewService.GetById(id);
            return bookReview != null ? Results.Ok(bookReview) : Results.NotFound("Book review not found.");
        });

        // Get book reviews by book id
        apiGroup.MapGet("/book-reviews/book/{bookId}",
            ([FromServices] IBookReviewService bookReviewService, long bookId) =>
            {
                var bookReviews = bookReviewService.GetByBookId(bookId);
                return bookReviews != null && bookReviews.Count > 0
                    ? Results.Ok(bookReviews)
                    : Results.NotFound("No book reviews found.");
            });

        // Get book reviews by user id
        apiGroup.MapGet("/book-reviews/user/{userId}",
            ([FromServices] IBookReviewService bookReviewService, long userId) =>
            {
                var bookReviews = bookReviewService.GetByUserId(userId);
                return bookReviews != null && bookReviews.Count > 0
                    ? Results.Ok(bookReviews)
                    : Results.NotFound("No book reviews found.");
            });

        // Add book review
        apiGroup.MapPost("/book-reviews",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookReviewService service,
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
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookReviewService bookReviewService,
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
            (HttpContext context, IAntiforgery antiforgery, [FromServices] IBookReviewService bookReviewService,
                long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = bookReviewService.Delete(id);
                return result ? Results.Ok("Book review deleted.") : Results.BadRequest("Book review not deleted.");
            });
    }
}