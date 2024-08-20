using Library.Entities.Implements;

namespace Library.Services.Interfaces;

public interface ILoanService : IService<Loan>
{
    List<Loan>? GetByUserId(long userId);
}