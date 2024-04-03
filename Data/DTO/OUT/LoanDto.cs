using Data.Model;

namespace Data.DTO;

public class LoanDto
{
    public int PersonCI { get; set; }
    public Person Person { get; set; }
    
    public float Amount { get; set; }
    public DateTime Date { get; set; } 
    public int Interest { get; set; }
    public int MonthsToPay { get; set; }
    public int PayDay { get; set; } // Dia del mes q se escoge para pagar
    public int Tax { get; set; }
    
}