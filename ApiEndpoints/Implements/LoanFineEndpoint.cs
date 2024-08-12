using Library.Entities.Implements;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements;



public class LoanFineEndpoint : IEndpoint
{
    public void DefineEndpoints(WebApplication application, RouteGroupBuilder apiGroup)
    {
        // Get all loan fines
        apiGroup.MapGet("/loanFines", ([FromServices] ILoanFineService service) =>
        {
            var loanFines = service.GetAll();
            return loanFines.Count > 0 ? Results.Ok(loanFines) : Results.NotFound("No loan fines found.");
        }).WithName("GetAllLoanFines");

        // Get loan fine by id
        apiGroup.MapGet("/loanFines/{id}", ([FromServices] ILoanFineService service, int id) =>
        {
            var loanFine = service.GetById(id);
            return loanFine != null ? Results.Ok(loanFine) : Results.NotFound("Loan fine not found.");
        }).WithName("GetLoanFineById");
        
        // Add loan fine
        apiGroup.MapPost("/loanFines", ([FromServices] ILoanFineService service, [FromForm] LoanFine loanFine) =>
        {
            var result = service.Add(loanFine);
            return result != null ? Results.Created($"/loanFines/{loanFine.Id}", loanFine) : Results.BadRequest("Loan fine not added.");
        }).WithName("AddLoanFine");
        
        // Update loan fine
        apiGroup.MapPut("/loanFines/{id}", ([FromServices] ILoanFineService service, int id, [FromForm] LoanFine loanFine) =>
        {
            var result = service.Update(id, loanFine);
            return result ? Results.Ok(loanFine) : Results.BadRequest("Loan fine not updated.");
        }).WithName("UpdateLoanFine");
        
        // Delete loan fine
        apiGroup.MapDelete("/loanFines/{id}", ([FromServices] ILoanFineService service, int id) =>
        {
            var result = service.Delete(id);
            return result ? Results.Ok("Loan fine deleted.") : Results.BadRequest("Loan fine not deleted.");
        }).WithName("DeleteLoanFine");
        
    }
}