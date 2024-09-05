using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface ILoanDetailRepository : IRepository<LoanDetail>
{
    Task<LoanDetail?> GetByLoanIdAndBookIdAsync(long loanId, long bookId);
    
    Task<List<LoanDetail>?> GetByLoanIdAsync(long loanId);
}