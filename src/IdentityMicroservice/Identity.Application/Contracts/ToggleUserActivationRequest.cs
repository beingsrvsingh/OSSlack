

namespace Identity.Application.Contracts;

public class ToggleUserActivationRequest

{
    public required string Email { get; set; }
    public required int PhoneNumber { get; set; }
    public required string FirebaseIdToken { get; set; }
}