using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class LoanFineService(ILoanFineRepository loanFineRepository) : ILoanFineService
{
    public List<LoanFine>? GetAll()
    {
        return loanFineRepository.GetAllAsync().Result;
    }

    public LoanFine? GetById(long id)
    {
        return loanFineRepository.GetByIdAsync(id).Result;
    }

    public LoanFine? Add(LoanFine entity)
    {
        return loanFineRepository.AddAsync(entity).Result;
    }

    public LoanFine? Update(long id, LoanFine entity)
    {
        return loanFineRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return loanFineRepository.DeleteAsync(id).Result;
    }

    public LoanFine? GetByLoanId(long id)
    {
        return loanFineRepository.GetByLoanIdAsync(id).Result;
    }
}