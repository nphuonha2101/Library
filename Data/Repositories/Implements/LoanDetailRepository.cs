using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories.Implements
{
    public class LoanDetailRepository : Repository<LoanDetail>, ILoanDetailRepository
    {
        public LoanDetailRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}