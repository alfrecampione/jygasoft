using Data.Model;

namespace Data.DTO;

public class PaymentDto
{
    
    public float Amount { get; set; }
    public int PaymentPeriod { get; set; }

    public DateOnly? PayDate { get; set; } // Fecha de pago
    
    public string Status { get; set; }
    
    public static PaymentDto FromEntity(Payment payment, DateOnly loanDate)
    {
        return new PaymentDto
        {
            Amount = MathF.Round(payment.Balance, 3),
            PaymentPeriod = payment.PaymentPeriod,
            PayDate = payment.PayDate,
            Status = payment.PayDate > (loanDate.AddDays(payment.PaymentPeriod)) ? "Late" : "On time"
        };
    }
}