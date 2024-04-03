using Data.Model;

namespace Data.DTO;

public class PaymentDto
{
    
    public float Amount { get; set; }
    public int PaymentPeriod { get; set; }

    public DateOnly PayDate { get; set; } // Fecha de pago
    
    public string Status { get; set; }
    
    public static PaymentDto FromEntity(Payment payment)
    {
        return new PaymentDto
        {
            Amount = payment.Amount,
            PaymentPeriod = payment.PaymentPeriod,
            PayDate = payment.PayDate,
            Status = payment.Status
        };
    }
}