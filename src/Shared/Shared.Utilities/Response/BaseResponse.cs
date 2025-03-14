namespace Shared.Utilities.Response
{
    public class BaseResponse
    {
        public bool Succeeded { get; init; }

        public object? Errors { get; init; }
    }
}
