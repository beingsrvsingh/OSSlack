using Shared.Domain.Entities;

namespace Shared.Application.Common.Services.Interfaces
{
    public interface ILoggingApiClient
    {
        Task AddLog(Log request);
    }
}
