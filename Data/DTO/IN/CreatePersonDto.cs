#nullable enable
namespace Data.DTO;

public class CreatePersonDto
{
    // ReSharper disable once InconsistentNaming
    public required string CI { get; set; }
    public required string Name { get; set; }
    public required string FatherLastName { get; set; }
    public required string MotherLastName { get; set; }
    public required float AmountBorrowed { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Email { get; set; }
    public DateOnly DateBorrowed { get; set; }
    public int DayOfPayment { get; set; }
    public int MonthsToPay { get; set; }
    public int InterestRate { get; set; }
}