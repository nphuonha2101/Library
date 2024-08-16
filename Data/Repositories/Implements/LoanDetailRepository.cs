using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements
{
    public class LoanDetailRepository(ApplicationDbContext appDbContext)
        : Repository<LoanDetail>(appDbContext), ILoanDetailRepository
    {
        public async Task<LoanDetail> GetByLoanIdAndBookIdAsync(int loanId, int bookId)
        {
            return await AppDbContext.LoanDetails.FirstAsync(loanDetail =>
                loanDetail.LoanId == loanId && loanDetail.BookId == bookId);
        }
    }
}