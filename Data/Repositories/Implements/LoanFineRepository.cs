using Library.Data.Repositories.Interfaces;
using Library.DatabaseContext;
using Library.Entities.Implements;

namespace Library.Data.Repositories.Implements;

public class LoanFineRepository : Repository<LoanFine>, ILoanFineRepository
{
    public LoanFineRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }
}
