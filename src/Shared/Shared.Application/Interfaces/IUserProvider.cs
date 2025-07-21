namespace Shared.Application.Interfaces
{
    public interface IUserProvider
    {
        string Email { get; }
        string UserId { get; }
        string UserName { get; }
        string Role { get; }
    }
}
