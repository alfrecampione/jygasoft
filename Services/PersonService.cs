using Data.DTO;

namespace Services;

public class PersonService: IPersonService
{
    public Task<int> PostPerson(CreatePersonDto createPersonDto)
    {
        throw new NotImplementedException();
    }

    public Task<PersonDto> GetPerson(int ci)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePerson(int ci, CreatePersonDto updatePersonDto)
    {
        throw new NotImplementedException();
    }

    public Task DeletePerson(int ci)
    {
        throw new NotImplementedException();
    }
}