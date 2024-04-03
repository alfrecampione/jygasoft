using Data.DTO;

namespace Services;

public class LoanService: ILoanService
{
    public Task<int> PostLoan(CreateLoanDto createLoanDto)
    {
        throw new NotImplementedException();
    }

    public Task<LoanDto> GetLoan(int loanId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLoan(int loanId, CreateLoanDto updateLoanDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteLoan(int loanId)
    {
        throw new NotImplementedException();
    }
}