namespace Identity.Application.Contracts
{
    public class CreateUserInfoRequest
    {
        public required string FirstName { get; init; }
        public string LastName { get; init; } = null!;
    }
}
