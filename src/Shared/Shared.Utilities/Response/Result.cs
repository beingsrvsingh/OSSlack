using System.Diagnostics.CodeAnalysis;
using Shared.Utilities.Interfaces;

namespace Shared.Utilities.Response
{
    public class Result : BaseResponse, IResult
    {
        public object? Data { get; init; }
        public Result(bool succeeded, object? data, object? errors)
        {
            Succeeded = succeeded;
            Errors = errors;
            Data = data;
        }

        public static Result Success()
            => new Result(true, Array.Empty<string>(), Array.Empty<string>());

        public static Result Success(object data)
            => new Result(true, data, Array.Empty<String>());

        public static Result Failure() => new Result(false, Array.Empty<String>(), Array.Empty<String>());

        public static Result Failure(object errors)
            => new Result(false, Array.Empty<String>(), errors);

        public static Result Failure(FailureResponse errors)
            => new Result(false, Array.Empty<String>(), errors);
    }

    public class Result<T> : BaseResponse, IResult<T>
    {
        public T? Data { get; init; }

        public Result(bool succeeded, T? data, object? errors)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = errors;
        }

        public static Result<T> Success(T data)
            => new Result<T>(true, data, Array.Empty<String>());

        public static Result<T> Failure(object errors)
            => new Result<T>(false, default, errors);

        public static Result<T> Failure(FailureResponse errors)
            => new Result<T>(false, default, errors);
    }    

    public class FailureResponse
    {
        [SetsRequiredMembers]
        public FailureResponse(string code, object description)
        {
            Code = code;
            Description = description;
        }
        public required string Code { get; set; }
        public required object Description { get; set; } = new();
    }
}
