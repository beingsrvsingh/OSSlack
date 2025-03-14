using Microsoft.AspNetCore.Identity;

namespace Shared.Utilities.Response.Extensions
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result, string userId)
        {
            return result.Succeeded
                ? Result.Success(userId)
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
