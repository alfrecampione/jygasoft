namespace Data.DTO;

public class CreatePaymentDto
{
    public int LoanId { get; set; }
    public int Amount { get; set; }
    public int PaymentPeriod { get; set; }
    public DateOnly PayDate { get; set; }
    public string Status { get; set; }
}