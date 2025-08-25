
namespace Shared.Application.Contracts
{
    public class PaymentInfoDto
    {
        public string Mode { get; set; } = String.Empty!;
        public string? CardType { get; set; } = null!;
        public string? Name { get; set; } = null!;
        public string? CardNumber { get; set; } = null!;
        public string? Status { get; set; } = null!;
    }
}