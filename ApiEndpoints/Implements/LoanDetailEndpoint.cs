using Library.Services.Interfaces;
using Library.Entities.Implements;
using Library.Services.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements
{
    public class LoanDetailEndpoint : IEndpoint
    {
        public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
        {
            // Get all loan details
            apiGroup.MapGet("/loanDetails", (LoanDetailService service) =>
            {
                var loanDetails = service.GetAll();
                return loanDetails.Count > 0 ? Results.Ok(loanDetails) : Results.NotFound("No loan details found.");
            }).WithName("GetAllLoanDetails");

            // Get loan detail by id
            apiGroup.MapGet("/loanDetails/{id}", (LoanDetailService service, int id) =>
            {
                var loanDetail = service.GetById(id);
                return loanDetail != null ? Results.Ok(loanDetail) : Results.NotFound("Loan detail not found.");
            }).WithName("GetLoanDetailById");

            // Add loan detail
            apiGroup.MapPost("/loanDetails", (LoanDetailService service, [FromForm] LoanDetail loanDetail) =>
            {
                var result = service.Add(loanDetail);
                return result != null ? Results.Created($"/loanDetails/{loanDetail.LoanId}", loanDetail) : Results.BadRequest("Loan detail not added.");
            }).WithName("AddLoanDetail");

            // Update loan detail
            apiGroup.MapPut("/loanDetails/{id}", (LoanDetailService service, int id, [FromForm] LoanDetail loanDetail) =>
            {
                var result = service.Update(id, loanDetail);
                return result ? Results.Ok(loanDetail) : Results.BadRequest("Loan detail not updated.");
            }).WithName("UpdateLoanDetail");

            // Delete loan detail
            apiGroup.MapDelete("/loanDetails/{id}", (LoanDetailService service, int id) =>
            {
                var result = service.Delete(id);
                return result ? Results.Ok("Loan detail deleted.") : Results.BadRequest("Loan detail not deleted.");
            }).WithName("DeleteLoanDetail");
        }
    }
}
