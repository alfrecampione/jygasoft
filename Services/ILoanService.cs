using Data.DTO;

namespace Services;

public interface ILoanService
{
    Task<int> PostLoan(CreateLoanDto createLoanDto);
    Task<LoanDto?> GetLoan(string ci);
    Task UpdateLoan(int loanId, UpdateLoanDto updateLoanDto);
    Task DeleteLoan(int loanId);
}