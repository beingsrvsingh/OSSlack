namespace Identity.Application.Contracts
{
    public class UpdateUserInfoRequest
    {
        public required string Id { get; set; }
        public required string FirstName { get; init; }
        public string LastName { get; init; } = null!;
    }
}
