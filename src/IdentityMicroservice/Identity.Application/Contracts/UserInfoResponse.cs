namespace Identity.Application.Contracts
{
    public class UserInfoResponse
    {
        public string Id { get; set; } = default!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
