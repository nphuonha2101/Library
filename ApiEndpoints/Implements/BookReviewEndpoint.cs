using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class BookReviewEndpoint: IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all book reviews
        apiGroup.MapGet("/book-reviews", ([FromServices] IBookReviewService bookReviewService) =>
        {
            var bookReviews = bookReviewService.GetAll();
            return bookReviews != null && bookReviews.Count > 0 ? Results.Ok(bookReviews) : Results.NotFound("No book reviews found.");
        });
        
        // Get book review by id
        apiGroup.MapGet("/book-reviews/{id}", ([FromServices] IBookReviewService bookReviewService, int id) =>
        {
            var bookReview = bookReviewService.GetById(id);
            return bookReview != null ? Results.Ok(bookReview) : Results.NotFound("Book review not found.");
        });
        
        // Get book reviews by book id
        apiGroup.MapGet("/book-reviews/book/{bookId}", ([FromServices] IBookReviewService bookReviewService, long bookId) =>
        {
            var bookReviews = bookReviewService.GetByBookId(bookId);
            return bookReviews != null && bookReviews.Count > 0 ? Results.Ok(bookReviews) : Results.NotFound("No book reviews found.");
        });
        
        // Get book reviews by user id
        apiGroup.MapGet("/book-reviews/user/{userId}", ([FromServices] IBookReviewService bookReviewService, long userId) =>
        {
            var bookReviews = bookReviewService.GetByUserId(userId);
            return bookReviews != null && bookReviews.Count > 0 ? Results.Ok(bookReviews) : Results.NotFound("No book reviews found.");
        });
        
        // Add book review
        apiGroup.MapPost("/book-reviews", ([FromServices] IBookReviewService bookReviewService, [FromBody] BookReviewDto bookReviewDto) =>
        {
            var result = bookReviewService.Add((BookReview) bookReviewDto.ToEntity());
            return result != null ? Results.Created($"/book-reviews/{result.Id}", result) : Results.BadRequest("Book review not added.");
        });
        
        // Update book review
        apiGroup.MapPut("/book-reviews/{id}", ([FromServices] IBookReviewService bookReviewService, long id, [FromBody] BookReviewDto bookReviewDto) =>
        {
            var result = bookReviewService.Update(id, (BookReview) bookReviewDto.ToEntity());
            return result != null ? Results.Ok(result) : Results.BadRequest("Book review not updated.");
        });
        
        // Delete book review
        apiGroup.MapDelete("/book-reviews/{id}", ([FromServices] IBookReviewService bookReviewService, long id) =>
        {
            var result = bookReviewService.Delete(id);
            return result ? Results.Ok("Book review deleted.") : Results.BadRequest("Book review not deleted.");
        });
    }
}