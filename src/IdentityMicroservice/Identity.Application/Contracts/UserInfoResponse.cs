namespace Identity.Application.Contracts
{
    public class UserInfoResponse
    {
        public required string FirstName { get; init; }
        public string LastName { get; init; } = null!;
    }
}
