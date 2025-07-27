
namespace Identity.Application.Contracts
{
    public class CountryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AlphaTwoCode { get; set; } = null!;
        public string Emoji { get; set; } = null!;
        public string Unicode { get; set; } = null!;
        public string DialCode { get; set; } = null!;
        public string ImageUri { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}