namespace Ordering.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPayment(decimal amount);
    }

    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
