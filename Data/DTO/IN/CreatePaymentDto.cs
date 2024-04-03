namespace Data.DTO;

public class CreatePaymentDto
{
    public string PersonCI { get; set; }
    public float Amount { get; set; }
    public DateOnly PayDate { get; set; }
}