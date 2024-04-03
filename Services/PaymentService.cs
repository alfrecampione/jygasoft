using Data.DTO;
using Data.Model;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class PaymentService: IPaymentService
{
    private readonly IDataRepository _dataRepository;
    
    public PaymentService(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }
    
    public async Task<int> PostPayment(CreatePaymentDto paymentDto)
    {
        var loan = await _dataRepository.Set<Loan>().FirstOrDefaultAsync(l=>l.Id == paymentDto.LoanId);
        if (loan == null)
        {
            throw new Exception("Loan not found");
        }
        var payment = new Payment()
        {
            LoanId = paymentDto.LoanId,
            Amount = paymentDto.Amount,
            PaymentPeriod = paymentDto.PaymentPeriod,
            PayDate = paymentDto.PayDate,
            Status = paymentDto.Status
        };
        await _dataRepository.Set<Payment>().Create(payment);
        await _dataRepository.Save(default);
        return payment.Id;
    }

    public async Task<PaymentDto?> GetPayment(int paymentId)
    {
        var payment = await _dataRepository.Set<Payment>().FirstOrDefaultAsync(p => p.Id == paymentId);
        return payment == null ? null : PaymentDto.FromEntity(payment);
    }

    public async Task<IEnumerable<PaymentDto>?> GetPayments(string personCI)
    {
        return await _dataRepository.Set<Payment>()
            .Include(p=>p.Loan)
            .ThenInclude(l=>l.Person)
            .Where(p => p.Loan.Person.CI == personCI)
            .Select(p => PaymentDto.FromEntity(p))
            .ToListAsync();
    }

    public async Task UpdatePayment(int paymentId, UpdatePaymentDto paymentDto)
    {
        var payment = await _dataRepository.Set<Payment>().FirstOrDefaultAsync(p => p.Id == paymentId);
        if (payment == null)
        {
            throw new Exception("Payment not found");
        }
        payment.Amount = paymentDto.Amount;
        payment.PaymentPeriod = paymentDto.PaymentPeriod;
        payment.PayDate = paymentDto.PayDate;
        payment.Status = paymentDto.Status;
        await _dataRepository.Save(default);
    }

    public async Task DeletePayment(int paymentId)
    {
        var payment = await _dataRepository.Set<Payment>().FirstOrDefaultAsync(p => p.Id == paymentId);
        if (payment == null)
        {
            throw new Exception("Payment not found");
        }
        _dataRepository.Set<Payment>().Remove(payment);
        await _dataRepository.Save(default);
    }
}