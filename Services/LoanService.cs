using Data.DTO;
using Data.Model;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class LoanService: ILoanService
{
    private readonly IDataRepository _dataRepository;
    
    public LoanService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    
    public async Task<int> PostLoan(CreateLoanDto createLoanDto)
    {
        var loan = new Loan
        {
            PersonCI = createLoanDto.PersonCI,
            Date = createLoanDto.Date,
            Amount = createLoanDto.Amount,
            MonthsToPay = createLoanDto.MonthsToPay,
            PayDay = createLoanDto.PayDay,
            InterestRate = createLoanDto.InterestRate
        };
        var person = await _dataRepository.Set<Person>().FirstOrDefaultAsync(p => p.CI == createLoanDto.PersonCI);
        if (person == null)
        {
            throw new Exception("Person not found");
        }
        loan.Person = person;
        await _dataRepository.Set<Loan>().Create(loan);
        await _dataRepository.Save(default);
        return loan.Id;
    }

    public async Task<LoanDto?> GetLoan(string ci)
    {
        var loan = await _dataRepository.Set<Loan>().Include(l => l.Person).FirstOrDefaultAsync(l => l.PersonCI == ci);
        return loan == null ? null : LoanDto.FromEntity(loan);
    }

    public async Task UpdateLoan(int loanId, UpdateLoanDto updateLoanDto)
    {
        var loan = await _dataRepository.Set<Loan>().FirstOrDefaultAsync(l => l.Id == loanId);
        if (loan == null)
            throw new Exception("Loan not found");
        loan.PersonCI = updateLoanDto.PersonCI;
        loan.Date = updateLoanDto.Date;
        loan.Amount = updateLoanDto.Amount;
        loan.MonthsToPay = updateLoanDto.MonthsToPay;
        loan.PayDay = updateLoanDto.PayDay;
        loan.InterestRate = updateLoanDto.InterestRate;
    }

    public Task DeleteLoan(int loanId)
    {
        var loan =  _dataRepository.Set<Loan>().FirstOrDefault(l => l.Id == loanId);
        if (loan == null)
            throw new Exception("Loan not found");
        _dataRepository.Set<Loan>().Remove(loan);
        return _dataRepository.Save(default);
    }
}