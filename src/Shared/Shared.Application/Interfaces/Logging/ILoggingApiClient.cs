using Shared.Domain.Entities;

namespace Shared.Application.Interfaces.Logging
{
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for logging API client.
    /// </summary>
    public interface ILoggingApiClient
    {
        Task AddLogAsync(BaseLog request);
    }
}
