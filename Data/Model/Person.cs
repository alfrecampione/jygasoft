using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Data.Model;

public class Person
{
    [Required]
    [MaxLength(11)]
    // ReSharper disable once InconsistentNaming
    public int CI { get; set; }
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    [MaxLength(200)]
    public string Email { get; set; }
    public int? Phone { get; set; }
    public int PayDay { get; set; }
    public int Tax { get; set; }
    public IEnumerable<Loan> Loans {get; set;}
}