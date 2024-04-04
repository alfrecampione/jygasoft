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

    public async Task<IEnumerable<PaymentDto>?> GetPayments(string personCI)
    {
        return await _dataRepository.Set<Payment>()
            .Include(p=>p.Loan)
            .ThenInclude(l=>l.Person)
            .Where(p => p.Loan.Person.CI == personCI)
            .Where(p=>p.Balance>0)
            .Select(p => PaymentDto.FromEntity(p, p.Loan.Date))
            .ToListAsync();
    }

    public async Task PostPayment(CreatePaymentDto paymentDto)
    {
        var loan = await _dataRepository.Set<Loan>()
            .Include(l=>l.Payments)
            .FirstOrDefaultAsync(l=>l.PersonCI == paymentDto.PersonCI);
        if (loan == null)
        {
            throw new Exception("Loan not found");
        }

        var payment = loan.Payments.FirstOrDefault(p => MathF.Abs(p.Amount - p.Balance) > 0);
        var copy = payment;
        if (copy == null)
        {
            throw new Exception("Loan already paid");
        }
        float amount = paymentDto.Amount;
        
        while (amount>0)
        {
            copy.Balance += amount;
            if (copy.Balance > copy.Amount )
            {
                amount = copy.Balance - copy.Amount;
                copy.Balance = copy.Amount;
                copy.PayDate = DateOnly.FromDateTime(DateTime.Now);
            }
            else
                break;
            
            payment = loan.Payments.FirstOrDefault(p => Math.Abs(p.Amount - p.Balance) > 0);
            if (payment == null)
            {
                if (amount > 0)
                {
                    throw new Exception("Amount exceeds loan");
                }
                await _dataRepository.Save(default);
                return;
            }
            copy = payment;
        }
        copy.PayDate = DateOnly.FromDateTime(DateTime.Now);
        
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