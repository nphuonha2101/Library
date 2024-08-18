using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface ILoanDetailRepository : IRepository<LoanDetail>
{
    Task<LoanDetail> GetByLoanIdAndBookIdAsync(int loanId, int bookId);
    
    Task<LoanDetail> GetByUserIdAsync(int userId);
}