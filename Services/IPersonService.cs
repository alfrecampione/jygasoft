using Data.DTO;

namespace Services;

public interface IPersonService
{
    public Task<int> PostPerson(CreatePersonDto createPersonDto);
    public Task<PersonDto> GetPerson(int ci);
    public Task UpdatePerson(int ci, CreatePersonDto updatePersonDto);
    public Task DeletePerson(int ci);
}