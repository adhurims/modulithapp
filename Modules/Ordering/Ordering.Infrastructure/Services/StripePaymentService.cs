using System.Threading.Tasks;
using Stripe;  
using Ordering.Application.Interfaces;

namespace Ordering.Domain.DomainServices
{
    public class StripePaymentService : IPaymentService
    {
        private readonly string _stripeApiKey;

        public StripePaymentService(string stripeApiKey)
        {
            _stripeApiKey = stripeApiKey;
            StripeConfiguration.ApiKey = _stripeApiKey;
        }

        public async Task<PaymentResult> ProcessPayment(decimal amount)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(amount * 100), // Convert amount to cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" },
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return new PaymentResult { IsSuccess = true };
            }
            catch (StripeException ex)
            {
                return new PaymentResult
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
