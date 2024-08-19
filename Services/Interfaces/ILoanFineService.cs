using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface ILoanFineService : IService<LoanFine>
{
    LoanFine GetByLoanId(long id);
}