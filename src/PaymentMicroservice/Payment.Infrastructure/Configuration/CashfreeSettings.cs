namespace Payment.Infrastructure.Configuration
{
    public class CashfreeSettings
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://sandbox.cashfree.com/pg";
    }

}
