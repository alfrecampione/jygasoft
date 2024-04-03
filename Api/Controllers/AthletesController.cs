using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AthletesController : ControllerBase
{
    private readonly IAthleteService _athleteService;
    private readonly ILogger<AthletesController> _logger;

    public AthletesController(ILogger<AthletesController> logger, IAthleteService athleteService)
    {
        _logger = logger;
        _athleteService = athleteService;
    }

    [Authorize(Roles = "Admin, Moderator")]
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreateAthleteDto createAthleteDto)
    {
        var newAthleteId = await _athleteService.PostAthlete(createAthleteDto);
        if (newAthleteId == -1)
            return BadRequest();
        var newAthlete = await _athleteService.GetAthlete(newAthleteId);
        return CreatedAtAction(nameof(Get), new { id = newAthleteId }, newAthlete);
    }

    [HttpGet("{id:int}/photo")]
    public async Task<IActionResult> GetImage(int id)
    {
        var (image, mimeType) = await _athleteService.GetAthleteImage(id);

        if (image != null) return File(image, mimeType!);

        return NotFound();
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var athlete = await _athleteService.GetAthlete(id);
        if (athlete != null)
            return Ok(athlete);
        return NotFound();
    }

    [Authorize(Roles = "Admin, Moderator")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromForm] CreateAthleteDto updateAthleteDto)
    {
        var athlete = await _athleteService.GetAthlete(id);
        if (athlete == null)
            return NotFound();
        await _athleteService.UpdateAthlete(id, updateAthleteDto);
        return NoContent();
    }

    [Authorize(Roles = "Admin, Moderator")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var athlete = await _athleteService.GetAthlete(id);
        if (athlete == null)
            return NotFound();
        await _athleteService.DeleteAthlete(id);
        return NoContent();
    }
}