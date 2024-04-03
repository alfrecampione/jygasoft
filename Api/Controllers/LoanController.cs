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
}