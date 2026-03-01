using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Shared.Utilities.Interfaces;

namespace Shared.Utilities.Response
{
    public class Result : BaseResponse, IResult
    {
        public object? Data { get; init; }

        [JsonConstructor]
        public Result(bool succeeded, object? data, IEnumerable<FailureResponse>? errors)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = errors?.ToArray() ?? Array.Empty<FailureResponse>();
        }

        public static Result Success()
        => new(true, null, null);

        public static Result Success(object data)
            => new(true, data, null);

        public static Result Failure(params FailureResponse[] errors)
            => new(false, null, errors);
    }

    public class Result<T> : BaseResponse, IResult<T>
    {
        public T? Data { get; init; }

        [JsonConstructor]
        public Result(bool succeeded, T? data, IReadOnlyList<FailureResponse> errors)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = errors;
        }

        public static Result<T> Success(T data)
        => new(true, data, null);

        public static Result<T> Failure(params FailureResponse[] errors)
            => new(false, default, errors);
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
