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
        if (personCI == -1)
            return BadRequest();
        var newPerson = await _personService.GetPerson(personCI);
        return CreatedAtAction(nameof(Post), new { ci = personCI }, newPerson);
    }
    
    [HttpGet("{ci:int}")]
    public async Task<IActionResult> Get(int ci)
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
    
}