using Priest.Application.Features.Commands;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface IPriestExpertiseService
    {
        Task<Result> CreatePriestExpertiseAsync(CreatePriestExpertiseCommand command);
        Task<Result> UpdatePriestExpertiseAsync(UpdatePriestExpertiseCommand command);
        Task<Result> DeletePriestExpertiseAsync(int id);
    }

}
