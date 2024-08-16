using Library.Entities.Implements;

namespace Library.Data.Repositories.Interfaces;

public interface ILoanFineRepository : IRepository<LoanFine>
{
    Task<LoanFine> GetByLoanIdAsync(int id);
}