

namespace Shared.Utilities.Interfaces
{
    public interface IHttpRequestService
    {
        string GetIpAddress { get; }

        string GetUserAgent { get; }
    }
}
