namespace Utilities.Services
{
    public interface ISecurityService
    {
        string GetIpAddress { get; }

        string GetUserAget { get; }
    }
}
