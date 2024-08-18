using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class LoanRepository : Repository<Loan>, ILoanRepository
{
    public LoanRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public async Task<Loan> GetByUserIdAsync(int userId)
    {
        return await AppDbContext.Loans.FirstAsync(loan => loan.UserId == userId);
    }
}