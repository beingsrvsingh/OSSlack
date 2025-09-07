using Priest.Application.Features.Commands;
using Shared.Utilities.Response;

namespace Priest.Application.Services
{
    public interface IPriestLanguageService
    {
        Task<Result> CreatePriestLanguageAsync(CreatePriestLanguageCommand command);
        Task<Result> UpdatePriestLanguageAsync(UpdatePriestLanguageCommand command);
        Task<Result> DeletePriestLanguageAsync(int id);
    }

}
