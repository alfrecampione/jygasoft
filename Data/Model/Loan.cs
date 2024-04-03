using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model;

public class Loan
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    // ReSharper disable once InconsistentNaming
    
    public int PersonCI { get; set; }
    public Person Person { get; set; }

    [Required]
    public float Amount { get; set; }
    public DateTime Date { get; set; } 
    public int Interest { get; set; }
    public int MonthsToPay { get; set; }
    public int PayDay { get; set; } // Dia del mes q se escoge para pagar
    public int Tax { get; set; }

    
    public IEnumerable<Payment> Payments { get; set; }
}