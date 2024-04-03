using Data.Model;

namespace Data.DTO;

public class LoanDto
{
    public string PersonCI { get; set; }
    
    public float Balance { get; set; }
    public DateOnly Date { get; set; } 
    public int MonthsToPay { get; set; }
    public int PayDay { get; set; } // Dia del mes q se escoge para pagar
    public int InterestRate { get; set; }
    
    public static LoanDto FromEntity(Loan loan) => new()
    {
        PersonCI = loan.PersonCI,
        Balance = loan.Amount,
        Date = loan.Date,
        MonthsToPay = loan.MonthsToPay,
        PayDay = loan.PayDay,
        InterestRate = loan.InterestRate
    };
}