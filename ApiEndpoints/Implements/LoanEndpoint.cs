using Library.Services.Interfaces;
using Library.Entities.Implements;
using Library.Services.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements
{
    public class LoanEndpoint : IEndpoint
    {
        public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
        {
            // Get all loans
            apiGroup.MapGet("/loans", (LoanService service) =>
            {
                var loans = service.GetAll();
                return loans.Count > 0 ? Results.Ok(loans) : Results.NotFound("No loans found.");
            }).WithName("GetAllLoans");

            // Get loan by id
            apiGroup.MapGet("/loans/{id}", (LoanService service, int id) =>
            {
                var loan = service.GetById(id);
                return loan != null ? Results.Ok(loan) : Results.NotFound("Loan not found.");
            }).WithName("GetLoanById");

            // Add loan
            apiGroup.MapPost("/loans", (LoanService service, [FromForm] Loan loan) =>
            {
                var result = service.Add(loan);
                return result != null ? Results.Created($"/loans/{loan.Id}", loan) : Results.BadRequest("Loan not added.");
            }).WithName("AddLoan");

            // Update loan
            apiGroup.MapPut("/loans/{id}", (LoanService service, int id, [FromForm] Loan loan) =>
            {
                var result = service.Update(id, loan);
                return result ? Results.Ok(loan) : Results.BadRequest("Loan not updated.");
            }).WithName("UpdateLoan");

            // Delete loan
            apiGroup.MapDelete("/loans/{id}", (LoanService service, int id) =>
            {
                var result = service.Delete(id);
                return result ? Results.Ok("Loan deleted.") : Results.BadRequest("Loan not deleted.");
            }).WithName("DeleteLoan");
        }
    }
}