using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class LoanFineEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all loan fines
        apiGroup.MapGet("/loan-fines", [Authorize]([FromServices] ILoanFineService service) =>
        {
            var loanFines = service.GetAll();
            return loanFines != null && loanFines.Count > 0
                ? Results.Ok(loanFines)
                : Results.NotFound("No loan fines found.");
        }).WithName("GetAllLoanFines");

        // Get loan fine by id
        apiGroup.MapGet("/loan-fines/{id}", [Authorize]([FromServices] ILoanFineService service, long id) =>
        {
            var loanFine = service.GetById(id);
            return loanFine != null ? Results.Ok(loanFine) : Results.NotFound("Loan fine not found.");
        }).WithName("GetLoanFineById");

        // Get loan fine by loan id
        apiGroup.MapGet("/loan-fines/loan/{loanId}",
            [Authorize]([FromServices] ILoanFineService service, long loanId) =>
            {
                var loanFine = service.GetByLoanId(loanId);
                return loanFine != null ? Results.Ok(loanFine) : Results.NotFound("Loan fine not found.");
            }).WithName("GetLoanFineByLoanId");

        // Add loan fine
        apiGroup.MapPost("/loan-fines",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanFineService service,
                [FromServices] ILoanService loanService,
                [FromForm] LoanFineDto loanFineDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Add((LoanFine)loanFineDto.ToEntity());

                if (result != null)
                {
                    var loan = loanService.GetById(result.LoanId);
                    result.Loan = loan;

                    Results.Created($"/loanFines/{result.Id}", result);
                    return;
                }

                Results.BadRequest("Loan fine not added.");
            }).WithName("AddLoanFine");


        // Update loan fine
        apiGroup.MapPut("/loan-fines/{id}",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanFineService service, long id,
                [FromServices] ILoanService loanService,
                [FromForm] LoanFineDto loanFineDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, (LoanFine)loanFineDto.ToEntity());

                if (result != null)
                {
                    var loan = loanService.GetById(result.LoanId);
                    result.Loan = loan;

                    Results.Ok(result);
                    return;
                }

                Results.BadRequest("Loan fine not updated.");
            }).WithName("UpdateLoanFine");

        // Delete loan fine
        apiGroup.MapDelete("/loan-fines/{id}",
            [Authorize](HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanFineService service,
                long id) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Delete(id);
                return result ? Results.Ok("Loan fine deleted.") : Results.BadRequest("Loan fine not deleted.");
            }).WithName("DeleteLoanFine");
    }
}