using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class LoanDetailService(ILoanDetailRepository loanDetailRepository) : ILoanDetailService
{
    public List<LoanDetail>? GetAll()
    {
        return loanDetailRepository.GetAllAsync().Result;
    }

    public LoanDetail GetById(long id)
    {
        throw new NotImplementedException();
    }

    public LoanDetail? Add(LoanDetail entity)
    {
        return loanDetailRepository.AddAsync(entity).Result;
    }

    public LoanDetail? Update(long id, LoanDetail entity)
    {
        return loanDetailRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(long id)
    {
        return loanDetailRepository.DeleteAsync(id).Result;
    }

    public LoanDetail? GetByLoanIdAndBookId(long loanId, long bookId)
    {
        return loanDetailRepository.GetByLoanIdAndBookIdAsync(loanId, bookId).Result;
    }
    
    public List<LoanDetail>? GetByLoanId(long loanId)
    {
        return loanDetailRepository.GetByLoanIdAsync(loanId).Result;
    }
}