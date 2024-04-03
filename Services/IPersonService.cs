using Data.DTO;

namespace Services;

public interface IPersonService
{
    public Task<string> PostPerson(CreatePersonDto createPersonDto);
    public Task<PersonDto?> GetPerson(string ci);
    public Task<IEnumerable<PersonDto>?> GetAllPersons();
    public Task UpdatePerson(string ci, UpdatePersonDto updatePersonDto);
    public Task DeletePerson(string ci);
}