using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class LoanService(ILoanRepository loanRepository) : ILoanService
{
    public List<Loan>? GetAll()
    {
        return loanRepository.GetAllAsync().Result;
    }

    public Loan? GetById(long id)
    {
        return loanRepository.GetByIdAsync(id).Result;
    }

    public Loan? Add(Loan entity)
    {
        return loanRepository.AddAsync(entity).Result;
    }

    public Loan? Update(long id, Loan entity)
    {
        return loanRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return loanRepository.DeleteAsync(id).Result;
    }
    
    public List<Loan>? GetByUserId(long userId)
    {
        return loanRepository.GetByUserIdAsync(userId).Result;
    }
}