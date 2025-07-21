namespace Identity.Application.Contracts
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; } = null!;
    }
}
