using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class LoanEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
    {
        // Get all loans
        apiGroup.MapGet("/loans", [Authorize(Roles = "admin")]([FromServices] ILoanService service) =>
        {
            var loans = service.GetAll();
            return loans != null && loans.Count > 0 ? Results.Ok(loans) : Results.NotFound("No loans found.");
        }).WithName("GetAllLoans");

        // Get loan by id
        apiGroup.MapGet("/loans/{id}", ([FromServices] ILoanService service, long id) =>
        {
            var loan = service.GetById(id);
            return loan != null ? Results.Ok(loan) : Results.NotFound("Loan not found.");
        }).WithName("GetLoanById");

        // Get loan by user id
        apiGroup.MapGet("/users/{userId}/loans", [Authorize]([FromServices] ILoanService service, long userId) =>
        {
            var loans = service.GetByUserId(userId);
            return loans != null ? Results.Ok(loans) : Results.NotFound("Loan not found.");
        }).WithName("GetLoansByUserId");

        // Add loan
        apiGroup.MapPost("/loans",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanService service,
                [FromServices] ILoanFineService loanFineService, [FromServices] IUserService userService,
                [FromForm] LoanDto loanDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Add((Loan)loanDto.ToEntity());

                if (result != null)
                {
                    if (result.LoanFineId != null && result.LoanFineId != 0)
                    {
                        var loanFineId = (long) result.LoanFineId;
                        var loanFine = loanFineService.GetById(loanFineId);
                        result.LoanFine = loanFine;
                    }

                    var user = userService.GetById(result.UserId);
                    result.User = user;
                }

                return result != null
                    ? Results.Created($"/loans/{result.Id}", result)
                    : Results.BadRequest("Loan not added.");
            }).WithName("AddLoan");

        // Update loan
        apiGroup.MapPut("/loans/{id}",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanService service
                , [FromServices] ILoanFineService loanFineService, [FromServices] IUserService userService , long id,
                [FromForm] LoanDto loanDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, (Loan)loanDto.ToEntity());
                
                if (result != null)
                {
                    if (result.LoanFineId != null && result.LoanFineId != 0)
                    {
                        var loanFineId = (long) result.LoanFineId;
                        var loanFine = loanFineService.GetById(loanFineId);
                        result.LoanFine = loanFine;
                    }

                    var user = userService.GetById(result.UserId);
                    result.User = user;
                } 
                
                return result != null ? Results.Ok(result) : Results.BadRequest("Loan not updated.");
            }).WithName("UpdateLoan");

        // Delete loan
        apiGroup.MapDelete("/loans/{id}",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanService service, long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Loan deleted.") : Results.BadRequest("Loan not deleted.");
            }).WithName("DeleteLoan");
    }
}