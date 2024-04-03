using Data.Model;

namespace Data.DTO;

public class PersonDto
{
    public string Ci { get; set; }
    public string FullName {get; set;}
    public string Email {get; set;}
    public int? InterestRate {get; set;}
    public float? StartAmount {get; set;}
    public float? PaidAmount {get; set;}
    // The total to pay considering the interestRate per monthsToPay
    public int? TotalAmount {get; set;}

    public static PersonDto FromEntity(Person person)
    {
        return new PersonDto()
        {
            Ci = person.CI,
            FullName = $"{person.Name} {person.FatherLastName} {person.MotherLastName}",
            Email = person.Email,
            InterestRate = person.Loan?.InterestRate,
            StartAmount = person.Loan?.Amount,
            PaidAmount = person.Loan?.Payments.Sum(p=>p.Balance),
            TotalAmount = person.Loan==null?null:(int)person.Loan.Payments.Sum(p=>p.Amount)
        };
    }
}