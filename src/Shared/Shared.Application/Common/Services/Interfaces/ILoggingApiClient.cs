using Shared.Domain.Entities;

namespace Shared.Application.Common.Services.Interfaces
{
    public interface ILoggingApiClient
    {
        Task AddLogAsync(BaseLog request);
    }
}
