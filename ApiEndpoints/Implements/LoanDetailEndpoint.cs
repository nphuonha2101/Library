using Library.Dto.Implements;
using Library.Services.Interfaces;
using Library.Entities.Implements;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Library.ApiEndpoints.Implements
{
    public class LoanDetailEndpoint : IEndpoint
    {
        public void DefineEndpoints(WebApplication app, RouteGroupBuilder apiGroup)
        {
            // Get all loan details
            apiGroup.MapGet("/loan-details", ([FromServices] ILoanDetailService service) =>
            {
                var loanDetails = service.GetAll();
                return loanDetails.Count > 0 ? Results.Ok(loanDetails) : Results.NotFound("No loan details found.");
            }).WithName("GetAllLoanDetails");

            // Get loan detail by id
            apiGroup.MapGet("/loan-details/by-ids",
                ([FromServices] ILoanDetailService service, [FromQuery] int bookId, [FromQuery] int loanId) =>
                {
                    var loanDetail = service.GetByLoanIdAndBookId(bookId: bookId, loanId: loanId);
                    return loanDetail != null ? Results.Ok(loanDetail) : Results.NotFound("Loan detail not found.");
                }).WithName("GetLoanDetailById");

            // Add loan detail
            apiGroup.MapPost("/loan-details",
                (HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanDetailService service,
                    [FromForm] LoanDetailDto loanDetailDto) =>
                {
                    antiforgery.ValidateRequestAsync(context);
                    var result = service.Add((LoanDetail)loanDetailDto.ToEntity());
                    return result != null
                        ? Results.Created($"/loanDetails/{result.LoanId}", result)
                        : Results.BadRequest("Loan detail not added.");
                }).WithName("AddLoanDetail");

            // Update loan detail
            apiGroup.MapPut("/loan-details/{id}",
                (HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanDetailService service, int id,
                    [FromForm] LoanDetailDto loanDetailDto) =>
                {
                    antiforgery.ValidateRequestAsync(context);
                    var result = service.Update(id, (LoanDetail)loanDetailDto.ToEntity());
                    return result ? Results.Ok(result) : Results.BadRequest("Loan detail not updated.");
                }).WithName("UpdateLoanDetail");

            // Delete loan detail
            apiGroup.MapDelete("/loan-details/{id}",
                (HttpContext context, IAntiforgery antiforgery, [FromServices] ILoanDetailService service, int id) =>
                {
                    antiforgery.ValidateRequestAsync(context);
                    var result = service.Delete(id);
                    return result ? Results.Ok("Loan detail deleted.") : Results.BadRequest("Loan detail not deleted.");
                }).WithName("DeleteLoanDetail");
        }
    }
}