using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class LoanDetailEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
    {
        // Get all loan details
        apiGroup.MapGet("/loan-details", [Authorize(Roles = "admin")]([FromServices] ILoanDetailService service) =>
        {
            var loanDetails = service.GetAll();
            return loanDetails != null && loanDetails.Count > 0
                ? Results.Ok(loanDetails)
                : Results.NotFound("No loan details found.");
        }).WithName("GetAllLoanDetails");

        // Get loan detail by id
        apiGroup.MapGet("/loan-details/by-ids",
            [Authorize]([FromServices] ILoanDetailService service, [FromQuery] long bookId, [FromQuery] long loanId) =>
            {
                var loanDetail = service.GetByLoanIdAndBookId(bookId: bookId, loanId: loanId);
                return loanDetail != null ? Results.Ok(loanDetail) : Results.NotFound("Loan detail not found.");
            }).WithName("GetLoanDetailById");

        // Get loan detail by loan id
        apiGroup.MapGet("/loan-details/loan/{loanId}",
            [Authorize]([FromServices] ILoanDetailService service, [FromQuery] long loanId) =>
            {
                var loanDetail = service.GetByLoanId(loanId);
                return loanDetail != null ? Results.Ok(loanDetail) : Results.NotFound("Loan details not found.");
            }).WithName("GetLoanDetailByUserId");

        // Add loan detail
        apiGroup.MapPost("/loan-details",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanDetailService service,
                [FromServices] IBookService bookService, [FromServices] ILoanService loanService) =>
            {
                antiforgery.ValidateRequestAsync(context);
                
                var form = context.Request.Form;
                
                var loanDetailDto = new LoanDetailDto(
                    long.Parse(form["loanId"].ToString()),
                    long.Parse(form["bookId"].ToString()),
                    int.Parse(form["quantity"].ToString()),
                    DateTime.Parse(form["dueDate"].ToString())
                );
                
                
                
                var result = service.Add((LoanDetail)loanDetailDto.ToEntity());

                if (result != null)
                {
                    var book = bookService.GetById(result.BookId);
                    result.Book = book;
                    var loan = loanService.GetById(result.LoanId);
                    result.Loan = loan;
                    Results.Created($"/loanDetails/{result.LoanId}", result);
                    return;
                }

                Results.BadRequest("Loan detail not added.");
            }).WithName("AddLoanDetail");

        // Update loan detail
        apiGroup.MapPut("/loan-details/{id}",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanDetailService service,
                [FromServices] IBookService bookService, [FromServices] ILoanService loanService,
                long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                
                var form = context.Request.Form;
                
                var loanDetailDto = new LoanDetailDto(
                    long.Parse(form["loanId"].ToString()),
                    long.Parse(form["bookId"].ToString()),
                    
                    int.Parse(form["quantity"].ToString()),
                    DateTime.Parse(form["dueDate"].ToString())
                );
                
                
                
                var result = service.Update(id, (LoanDetail)loanDetailDto.ToEntity());

                if (result != null)
                {
                    var book = bookService.GetById(result.BookId);
                    result.Book = book;
                    var loan = loanService.GetById(result.LoanId);
                    result.Loan = loan;

                    Results.Ok(result);
                    return;
                }

                Results.BadRequest("Loan detail not updated.");
            }).WithName("UpdateLoanDetail");

        // Delete loan detail
        apiGroup.MapDelete("/loan-details/{id}",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanDetailService service,
                long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Loan detail deleted.") : Results.BadRequest("Loan detail not deleted.");
            }).WithName("DeleteLoanDetail");
    }
}