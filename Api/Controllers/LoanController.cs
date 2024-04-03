using Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController:ControllerBase
{
    private readonly ILoanService _loanService;
    private readonly ILogger<LoanController> _logger;
    
    public LoanController(ILoanService loanService, ILogger<LoanController> logger)
    {
        _loanService = loanService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> PostLoan([FromForm] CreateLoanDto createLoanDto)
    {
        var loan = await _loanService.PostLoan(createLoanDto);
        if (loan == -1)
            return BadRequest();
        return Ok(loan);
    }
    [HttpGet("{ci}")]
    public async Task<IActionResult> GetLoan(string ci)
    {
        var loan = await _loanService.GetLoan(ci);
        if (loan == null)
            return NotFound();
        return Ok(loan);
    }
    [HttpPut]
    public async Task<IActionResult> UpdateLoan(int id, [FromForm] UpdateLoanDto updateLoanDto)
    {
        await _loanService.UpdateLoan(id, updateLoanDto);
        return Ok();
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        await _loanService.DeleteLoan(id);
        return Ok();
    }
}