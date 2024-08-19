using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class LoanFineService : ILoanFineService
{
    private readonly ILoanFineRepository _loanfineRepository;

    public LoanFineService(ILoanFineRepository loanfineRepository)
    {
        _loanfineRepository = loanfineRepository;
    }

    public List<LoanFine> GetAll()
    {
        return _loanfineRepository.GetAllAsync().Result;
    }

    public LoanFine GetById(long id)
    {
        return _loanfineRepository.GetByIdAsync(id).Result;
    }

    public LoanFine Add(LoanFine entity)
    {
        return _loanfineRepository.AddAsync(entity).Result;
    }

    public bool Update(long id, LoanFine entity)
    {
        return _loanfineRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return _loanfineRepository.DeleteAsync(id).Result;
    }

    public LoanFine GetByLoanId(long id)
    {
        return _loanfineRepository.GetByLoanIdAsync(id).Result;
    }
}