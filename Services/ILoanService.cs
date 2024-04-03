using Data.DTO;

namespace Services;

public interface ILoanService
{
    Task<int> PostLoan(CreateLoanDto createLoanDto);
    Task<LoanDto?> GetLoan(string ci);
    Task UpdateLoan(UpdateLoanDto updateLoanDto);
    Task DeleteLoan(string ci);
}