using Priest.Application.Features.Commands;
using PriestMicroservice.Domain.Entities;
using Shared.Domain.Entities.Base;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface IConsultationModeService
    {
        Task<IEnumerable<ConsultationMode>> GetAllAsync();
        Task<Result> CreateConsultationModeAsync(CreateConsultationModeCommand command);
        Task<Result> UpdateConsultationModeAsync(UpdateConsultationModeCommand command);
        Task<Result> DeleteConsultationModeAsync(int id);
        Task<BasePrice> GetPriestExpertiseModeIdPrice(int entityId, int modeId);
    }

}
