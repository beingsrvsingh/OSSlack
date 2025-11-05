namespace Shared.Application.Common.Contracts.Response
{
    public class AddonResponseDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = "INR";
    }
}
