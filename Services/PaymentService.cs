using Data.DTO;

namespace Services;

public class PaymentService: IPaymentService
{
    public Task<int> PostPayment(PaymentDto paymentDto)
    {
        throw new NotImplementedException();
    }

    public Task<PaymentDto> GetPayment(int paymentId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PaymentDto>> GetPayments(int loanId)
    {
        throw new NotImplementedException();
    }

    public Task<int> PutPayment(int paymentId, PaymentDto paymentDto)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeletePayment(int paymentId)
    {
        throw new NotImplementedException();
    }
}