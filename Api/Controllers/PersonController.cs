using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Data.DTO;
// ReSharper disable InconsistentNaming

namespace Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PersonController:ControllerBase
{
    private readonly IPersonService _personService;
    private readonly ILogger<PersonController> _logger;
    public PersonController(ILogger<PersonController> logger,IPersonService personService)
    {
        _personService = personService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreatePersonDto createPersonDto)
    {
        var personCI = await _personService.PostPerson(createPersonDto);
        if (personCI == "")
            return BadRequest();
        var newPerson = await _personService.GetPerson(personCI);
        return CreatedAtAction(nameof(Post), new { ci = personCI }, newPerson);
    }
    
    [HttpGet("{ci}")]
    public async Task<IActionResult> Get(string ci)
    {
        var person = await _personService.GetPerson(ci);
        if (person != null)
            return Ok(person);
        return NotFound();
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var persons = await _personService.GetAllPersons();
        return Ok(persons);
    }
    [HttpPut("{ci}")]
    public async Task<IActionResult> Update(string ci, [FromForm] UpdatePersonDto updatePersonDto)
    {
        var person = await _personService.GetPerson(ci);
        if (person == null)
            return NotFound();
        await _personService.UpdatePerson(ci, updatePersonDto);
        return Ok();
    }
    [HttpDelete("{ci}")]
    public async Task<IActionResult> Delete(string ci)
    {
        var person = await _personService.GetPerson(ci);
        if (person == null)
            return NotFound();
        await _personService.DeletePerson(ci);
        return Ok();
    }
}