using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface ILoanDetailService : IService<LoanDetail>
{
    LoanDetail GetByLoanIdAndBookId(int loanId, int bookId);
}