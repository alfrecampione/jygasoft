using Data.DTO;

namespace Services;

public interface IPaymentService
{
    public Task<int> PostPayment(CreatePaymentDto paymentDto);
    public Task<PaymentDto?> GetPayment(int paymentId);
    public Task<IEnumerable<PaymentDto>?> GetPayments(string personCI);
    public Task UpdatePayment(int paymentId, UpdatePaymentDto paymentDto);
    public Task DeletePayment(int paymentId);
}