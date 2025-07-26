namespace Identity.Application.Contracts;

public class DeactivateUserRequest
{
    public required string UserId { get; set; } = default!;
    public required bool IsActive { get; set; }
}