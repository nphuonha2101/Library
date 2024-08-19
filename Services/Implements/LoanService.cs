using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public List<Loan> GetAll()
    {
        return _loanRepository.GetAllAsync().Result;
    }

    public Loan GetById(long id)
    {
        return _loanRepository.GetByIdAsync(id).Result;
    }

    public Loan Add(Loan entity)
    {
        return _loanRepository.AddAsync(entity).Result;
    }

    public bool Update(long id, Loan entity)
    {
        return _loanRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return _loanRepository.DeleteAsync(id).Result;
    }
}