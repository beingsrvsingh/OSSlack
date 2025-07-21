
namespace Shared.Utilities.Interfaces
{
    public interface IResult
    {
        bool Succeeded { get; }
        object? Errors { get; }
    }

    public interface IResult<T> : IResult
    {
        T? Data { get; }
    }
}
