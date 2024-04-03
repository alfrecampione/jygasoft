using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController: ControllerBase
{
    public readonly IPaymentService _paymentService;
    public readonly ILogger<PaymentController> _logger;
    
    public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPayments(int id)
    {
        var payments = await _paymentService.GetPayments(id);
        return Ok(payments);
    }
}