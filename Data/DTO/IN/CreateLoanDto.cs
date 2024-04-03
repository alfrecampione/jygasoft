namespace Data.DTO;

public class CreateLoanDto
{
    public string PersonCI { get; set; }
    public DateOnly Date { get; set; }
    public float Amount { get; set; }
    public int MonthsToPay { get; set; }
    public int PayDay { get; set; }
    public int InterestRate { get; set; }
}