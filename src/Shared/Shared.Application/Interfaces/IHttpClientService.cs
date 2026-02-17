using Shared.Domain.Enums;

namespace Shared.Application.Interfaces
{
    public interface IHttpClientService
    {
        Task<TResponse?> GetAsync<TResponse>(
            Microservice microservice,
            string endpoint,
            CancellationToken cancellationToken = default);

        Task<TResponse?> PostAsync<TRequest, TResponse>(
            Microservice microservice,
            string endpoint,
            TRequest data,
            CancellationToken cancellationToken = default);

        Task<TResponse?> PutAsync<TRequest, TResponse>(
            Microservice microservice,
            string endpoint,
            TRequest data,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(
            Microservice microservice,
            string endpoint,
            CancellationToken cancellationToken = default);
    }

}
