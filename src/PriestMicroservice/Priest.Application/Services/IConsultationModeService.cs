using Priest.Application.Features.Commands;
using Priest.Domain.Entities;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface IConsultationModeService
    {
        Task<IEnumerable<ConsultationMode>> GetAllAsync();
        Task<Result> CreateConsultationModeAsync(CreateConsultationModeCommand command);
        Task<Result> UpdateConsultationModeAsync(UpdateConsultationModeCommand command);
        Task<Result> DeleteConsultationModeAsync(int id);
    }

}
