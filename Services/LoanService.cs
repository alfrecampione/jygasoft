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
            InterestRate = createLoanDto.InterestRate,
            Payments = new Payment[createLoanDto.MonthsToPay]
        };
        int i = 1;
        loan.Payments = loan.Payments.Select(p => new Payment
        {
            Balance = 0,
            Amount = (loan.Amount / loan.MonthsToPay)*(1+loan.InterestRate),
            PaymentPeriod = i++,
            PayDate = null,
            Loan = loan,
            LoanId = loan.Id
        }).ToArray();
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

    public async Task UpdateLoan(UpdateLoanDto updateLoanDto)
    {
        var loan = await _dataRepository.Set<Loan>()
            .Include(l=>l.Payments)
            .FirstOrDefaultAsync(l => l.PersonCI==updateLoanDto.PersonCI);
        if (loan == null)
            throw new Exception("Loan not found");
        loan.PersonCI = updateLoanDto.PersonCI;
        loan.Date = updateLoanDto.Date;
        loan.Amount = updateLoanDto.Amount;
        loan.MonthsToPay = updateLoanDto.MonthsToPay;
        loan.PayDay = updateLoanDto.PayDay;
        loan.InterestRate = updateLoanDto.InterestRate;

        foreach (var payment in loan.Payments)
        {
            payment.Amount =
                MathF.Round((loan.Amount / loan.MonthsToPay) * (1 + (loan.InterestRate * loan.MonthsToPay) / 100f), 3);
        }
        await _dataRepository.Save(default);
    }

    public async Task DeleteLoan(string ci)
    {
        var loan =  await _dataRepository.Set<Loan>().FirstOrDefaultAsync(l => l.PersonCI == ci);
        if (loan == null)
            throw new Exception("Loan not found");
        _dataRepository.Set<Loan>().Remove(loan);
        await _dataRepository.Save(default);
    }
}