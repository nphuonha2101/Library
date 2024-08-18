using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface ILoanRepository : IRepository<Loan>
{
    Task<Loan> GetByUserIdAsync(int userId);
}