using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Payment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public int LoanId { get; set; }
    public Loan Loan { get; set; }
    
    public float Amount { get; set; }
    public float Balance { get; set; }
    public int PaymentPeriod { get; set; }

    public DateOnly? PayDate { get; set; }
}