using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface ILoanService : IService<Loan>
{
    Loan GetByUserId(int userId);
}