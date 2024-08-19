using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface ILoanRepository : IRepository<Loan>
{
    Task<List<Loan>?> GetByUserIdAsync(long userId);
}