using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;

namespace Library.Data.Repositories.Implements;

public class LoanRepository : Repository<Loan>, ILoanRepository
{
    public LoanRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}