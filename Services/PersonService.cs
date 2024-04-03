using Data.DTO;
using Data.Model;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class PersonService: IPersonService
{
    private readonly IDataRepository _dataRepository;
    
    public PersonService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    public async Task<string> PostPerson(CreatePersonDto createPersonDto)
    {
        if (await _dataRepository.Set<Person>().AnyAsync(p => p.CI == createPersonDto.CI))
            return "";
        var person = new Person
        {
            CI = createPersonDto.CI,
            Name = createPersonDto.Name,
            FatherLastName = createPersonDto.FatherLastName,
            MotherLastName = createPersonDto.MotherLastName,
            Email = createPersonDto.Email,
            Phone = createPersonDto.PhoneNumber,
        };
        await _dataRepository.Set<Person>().Create(person);
        await _dataRepository.Save(default);
        var loan = new Loan()
        {
            Amount = createPersonDto.AmountBorrowed,
            Date = createPersonDto.DateBorrowed,
            InterestRate = createPersonDto.InterestRate,
            MonthsToPay = createPersonDto.MonthsToPay,
            PayDay = createPersonDto.DayOfPayment,
            Person = person,
            PersonCI = person.CI,
            Payments = new Payment[createPersonDto.MonthsToPay]
        };
        int i = 1;
        loan.Payments = loan.Payments.Select(p => new Payment
        {
            Amount = MathF.Round((loan.Amount / loan.MonthsToPay)*(1+(loan.InterestRate*loan.MonthsToPay)/100f),3),
            Balance = 0,
            PaymentPeriod = i++,
            PayDate = null,
            Status = "Pending",
            Loan = loan,
            LoanId = loan.Id
        }).ToArray();
        
        await _dataRepository.Set<Loan>().Create(loan);
        await _dataRepository.Save(default);
        return person.CI;
    }

    public async Task<PersonDto?> GetPerson(string ci)
    {
        var person = await _dataRepository.Set<Person>()
            .Include(p=>p.Loan)
            .ThenInclude(l=>l.Payments)
            .FirstOrDefaultAsync(p=>p.CI==ci);
        return person == null ? null : PersonDto.FromEntity(person);
    }

    public async Task<IEnumerable<PersonDto>?> GetAllPersons()
    {
        var persons = await _dataRepository.Set<Person>()
            .Include(p=>p.Loan)
            .ThenInclude(l=>l.Payments)
            .ToListAsync();
        return persons.Select(PersonDto.FromEntity);
    }

    public async Task UpdatePerson(string ci, UpdatePersonDto updatePersonDto)
    {
        var person = await _dataRepository.Set<Person>().FirstOrDefaultAsync(p=>p.CI==ci);
        if (person == null) throw new Exception("Person not found");
        
        person.Name = updatePersonDto.Name;
        person.FatherLastName = updatePersonDto.FatherLastName;
        person.MotherLastName = updatePersonDto.MotherLastName;
        person.Email = updatePersonDto.Email;
        person.Phone = updatePersonDto.Phone;
        
        await _dataRepository.Save(default);
    }

    public async Task DeletePerson(string ci)
    {
        var person = await _dataRepository.Set<Person>().FirstOrDefaultAsync(p=>p.CI==ci);
        if (person == null) throw new Exception("Person not found");
        _dataRepository.Set<Person>().Remove(person);
        await _dataRepository.Save(default);
    }
}