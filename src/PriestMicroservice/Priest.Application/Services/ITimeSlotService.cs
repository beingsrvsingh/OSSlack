using Priest.Application.Features.Commands;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface ITimeSlotService
    {
        Task<Result> CreateTimeSlotAsync(CreateTimeSlotCommand command);
        Task<Result> UpdateTimeSlotAsync(UpdateTimeSlotCommand command);
        Task<Result> DeleteTimeSlotAsync(int id);
    }

}
