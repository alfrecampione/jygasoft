using Data.DTO;

namespace Services;

public interface IPaymentService
{
    public Task<int> PostPayment(PaymentDto paymentDto);
    public Task<PaymentDto> GetPayment(int paymentId);
    public Task<IEnumerable<PaymentDto>> GetPayments(int loanId);
    public Task<int> UpdatePayment(int paymentId, PaymentDto paymentDto);
    public Task<int> DeletePayment(int paymentId);
}