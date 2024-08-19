using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class LoanRepository(ApplicationDbContext appDbContext) : Repository<Loan>(appDbContext), ILoanRepository
{
    public async Task<List<Loan>?> GetByUserIdAsync(long userId)
    {
        return await AppDbContext.Loans.Where(x => x.UserId == userId).ToListAsync();
    }
}