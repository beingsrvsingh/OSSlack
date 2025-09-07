using Priest.Application.Features.Commands;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface IScheduleService
    {
        Task<Result> CreateScheduleAsync(CreateScheduleCommand command);
        Task<Result> UpdateScheduleAsync(UpdateScheduleCommand command);
        Task<Result> DeleteScheduleAsync(int id);
    }

}
