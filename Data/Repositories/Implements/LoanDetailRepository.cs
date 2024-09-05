using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class LoanDetailRepository(ApplicationDbContext appDbContext)
    : Repository<LoanDetail>(appDbContext), ILoanDetailRepository
{
    public async Task<LoanDetail?> GetByLoanIdAndBookIdAsync(long loanId, long bookId)
    {
        return await AppDbContext.LoanDetails.FirstAsync(loanDetail =>
            loanDetail.LoanId == loanId && loanDetail.BookId == bookId);
    }

    public async Task<List<LoanDetail>?> GetByLoanIdAsync(long loanId)
    {
        return await AppDbContext.LoanDetails.Where(loanDetail => loanDetail.LoanId == loanId).ToListAsync();
    }
}