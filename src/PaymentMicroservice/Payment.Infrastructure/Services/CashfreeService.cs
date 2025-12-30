using Microsoft.Extensions.Options;
using Payment.Application.Contracts;
using Payment.Application.Services;
using Payment.Infrastructure.Configuration;
using System.Text;
using System.Text.Json;

namespace Payment.Infrastructure.Services
{
    public class CashfreeService : ICashfreeService
    {
        private readonly HttpClient _http;
        private readonly CashfreeSettings _settings;

        public CashfreeService(HttpClient http, IOptions<CashfreeSettings> options)
        {
            _http = http;
            _settings = options.Value;
        }

        public async Task<CreatePaymentResponse> CreateOrderAsync(
            string orderId,
            decimal amount,
            string customerId,
            string customerEmail,
            string customerPhone)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_settings.BaseUrl}/orders"
            );

            request.Headers.Add("x-client-id", _settings.ClientId);
            request.Headers.Add("x-client-secret", _settings.ClientSecret);
            request.Headers.Add("x-api-version", "2022-09-01");

            var payload = new
            {
                order_id = orderId,
                order_amount = amount,
                order_currency = "INR",
                customer_details = new
                {
                    customer_id = customerId,
                    customer_email = customerEmail,
                    customer_phone = customerPhone
                }
            };

            request.Content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var doc = JsonDocument.Parse(json);

            return new CreatePaymentResponse(
                OrderId: orderId,
                OrderToken: doc.RootElement.GetProperty("order_token").GetString()!,
                Amount: amount
            );
        }
    }

}
