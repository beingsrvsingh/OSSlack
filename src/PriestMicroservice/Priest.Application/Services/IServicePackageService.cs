using Priest.Application.Features.Commands;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface IServicePackageService
    {
        Task<Result> CreateServicePackageAsync(CreateRitualServicePackageCommand command);
        Task<Result> UpdateServicePackageAsync(UpdateRitualServicePackageCommand command);
        Task<Result> DeleteServicePackageAsync(int id);
    }

}
