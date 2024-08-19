using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements;

public class LoanFineRepository(ApplicationDbContext appDbContext)
    : Repository<LoanFine>(appDbContext), ILoanFineRepository
{
    public async Task<LoanFine> GetByLoanIdAsync(long id)
    {
        return await AppDbContext.LoanFines.FirstAsync(lf => lf.LoanId == id);
    }
}