using Library.Dto.Implements;
using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;

public class LoanFineEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all loan fines
        apiGroup.MapGet("/loan-fines", ([FromServices] ILoanFineService service) =>
        {
            var loanFines = service.GetAll();
            return loanFines.Count > 0 ? Results.Ok(loanFines) : Results.NotFound("No loan fines found.");
        }).WithName("GetAllLoanFines");

        // Get loan fine by id
        apiGroup.MapGet("/loan-fines/{id}", ([FromServices] ILoanFineService service, int id) =>
        {
            var loanFine = service.GetById(id);
            return loanFine != null ? Results.Ok(loanFine) : Results.NotFound("Loan fine not found.");
        }).WithName("GetLoanFineById");

        // Get loan fine by loan id
        apiGroup.MapGet("/loan-fines/loan/{id}", ([FromServices] ILoanFineService service, int id) =>
        {
            var loanFine = service.GetByLoanId(id);
            return loanFine != null ? Results.Ok(loanFine) : Results.NotFound("Loan fine not found.");
        }).WithName("GetLoanFineByLoanId");

        // Add loan fine
        apiGroup.MapPost("/loan-fines",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanFineService service,
                [FromForm] LoanFineDto loanFineDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Add((LoanFine)loanFineDto.ToEntity());
                return result != null
                    ? Results.Created($"/loanFines/{result.Id}", result)
                    : Results.BadRequest("Loan fine not added.");
            }).WithName("AddLoanFine");


        // Update loan fine
        apiGroup.MapPut("/loan-fines/{id}",
            (HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanFineService service, int id, [FromForm] LoanFineDto loanFineDto) =>
            {
                antiforgery.ValidateRequestAsync(context);
                var result = service.Update(id, (LoanFine)loanFineDto.ToEntity());
                return result ? Results.Ok(result) : Results.BadRequest("Loan fine not updated.");
            }).WithName("UpdateLoanFine");

        // Delete loan fine
        apiGroup.MapDelete("/loan-fines/{id}", (HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanFineService service, int id) =>
        {
            antiforgery.ValidateRequestAsync(context);
            var result = service.Delete(id);
            return result ? Results.Ok("Loan fine deleted.") : Results.BadRequest("Loan fine not deleted.");
        }).WithName("DeleteLoanFine");
    }
}