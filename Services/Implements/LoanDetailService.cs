using Library.Data.Repositories.Interfaces;
using Library.Entities.Implements;
using Library.Services.Interfaces;

namespace Library.Services.Implements;

public class LoanDetailService : ILoanDetailService
{
    private readonly ILoanDetailRepository _loanDetailRepository;

    public LoanDetailService(ILoanDetailRepository loanDetailRepository)
    {
        _loanDetailRepository = loanDetailRepository;
    }

    public List<LoanDetail> GetAll()
    {
        return _loanDetailRepository.GetAllAsync().Result;
    }

    public LoanDetail GetById(int id)
    {
        throw new NotImplementedException();
    }

    public LoanDetail Add(LoanDetail entity)
    {
        return _loanDetailRepository.AddAsync(entity).Result;
    }

    public bool Update(int id, LoanDetail entity)
    {
        return _loanDetailRepository.UpdateAsync(id, entity).Result;
    }

    public bool Delete(int id)
    {
        return _loanDetailRepository.DeleteAsync(id).Result;
    }

    public LoanDetail GetByLoanIdAndBookId(int loanId, int bookId)
    {
        return _loanDetailRepository.GetByLoanIdAndBookIdAsync(loanId, bookId).Result;
    }
    
    public LoanDetail GetByUserId(int userId)
    {
        return _loanDetailRepository.GetByUserIdAsync(userId).Result;
    }
}