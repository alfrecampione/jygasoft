using Data.DTO;

namespace Services;

public interface IPaymentService
{
    public Task<IEnumerable<PaymentDto>?> GetPayments(string personCI);
    public Task PostPayment(CreatePaymentDto paymentDto);
    public Task DeletePayment(int paymentId);
}