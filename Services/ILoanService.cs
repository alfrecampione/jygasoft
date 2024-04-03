using Data.DTO;

namespace Services;

public interface ILoanService
{
    Task<int> PostLoan(CreateLoanDto createLoanDto);
    Task<LoanDto?> GetLoan(int loanId);
    Task UpdateLoan(int loanId, UpdateLoanDto updateLoanDto);
    Task DeleteLoan(int loanId);
}