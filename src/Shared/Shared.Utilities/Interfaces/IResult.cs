
using Shared.Utilities.Response;

namespace Shared.Utilities.Interfaces
{
    public interface IResult
    {
        bool Succeeded { get; }
        IReadOnlyList<FailureResponse> Errors { get; }
    }

    public interface IResult<T> : IResult
    {
        T? Data { get; }
    }
}
