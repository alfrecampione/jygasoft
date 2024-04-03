namespace Data.DTO;

public class UpdatePaymentDto
{
    public float Amount { get; set; }
    public int PaymentPeriod { get; set; }
    public DateOnly PayDate { get; set; }
    public string Status { get; set; }
}