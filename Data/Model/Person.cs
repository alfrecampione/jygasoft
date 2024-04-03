using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Data.Model;

public class Person
{
    [Required]
    [MaxLength(11)]
    // ReSharper disable once InconsistentNaming
    public string CI { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FatherLastName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string MotherLastName { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Email { get; set; }
    [MaxLength(32)]
    public string Phone { get; set; }
    public Loan Loan {get; set;}
}