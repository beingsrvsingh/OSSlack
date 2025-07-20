using System.Diagnostics.CodeAnalysis;

namespace Shared.Utilities.Response
{
    public class Result : BaseResponse
    {
        internal Result(bool succeeded, object? data, object? errors)
        {
            Succeeded = succeeded;
            Errors = errors;
            Data = data;
        }

        public object? Data { get; init; }

        public static Result Success()
        {
            return new Result(true, null, Array.Empty<string>());
        }

        public static Result Success(object data)
        {
            return new Result(true, data, null);
        }

        public static Result Failure()
        {
            return new Result(false, null, null);
        }

        public static Result Failure(object errors)
        {
            return new Result(false, null, errors);
        }

        public static Result Failure(FailureResponse errors)
        {
            return new Result(false, null, errors);
        }
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
