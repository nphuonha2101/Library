using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}