using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Payment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int LoanId { get; set; }
    public Loan Loan { get; set; }
    
    public int Amount { get; set; }
    public int PaymentPeriod { get; set; }

    public DateTime PayDate { get; set; } // Fecha de pago
    
    public string Status { get; set; }
}