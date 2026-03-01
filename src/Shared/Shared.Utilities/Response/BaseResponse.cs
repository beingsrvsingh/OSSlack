namespace Shared.Utilities.Response
{
    public abstract class BaseResponse
    {
        public bool Succeeded { get; init; }

        public IReadOnlyList<FailureResponse> Errors { get; init; } = Array.Empty<FailureResponse>();
    }
}
