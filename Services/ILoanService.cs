using Data.DTO;

namespace Services;

public interface ILoanService
{
    public Task<int> PostLoan(CreateLoanDto createLoanDto);
    public Task<LoanDto> GetLoan(int loanId);
    public Task UpdateLoan(int loanId, CreateLoanDto updateLoanDto);
    public Task DeleteLoan(int loanId);
}