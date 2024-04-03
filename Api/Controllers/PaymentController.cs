using Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController: ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ILogger<PaymentController> _logger;
    
    public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
    {
        _paymentService = paymentService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] CreatePaymentDto paymentDto)
    {
        await _paymentService.PostPayment(paymentDto);
        return Ok();
    }
    
    [HttpGet("{ci}")]
    public async Task<IActionResult> GetPayments(string ci)
    {
        var payments = await _paymentService.GetPayments(ci);
        return Ok(payments);
    }
}