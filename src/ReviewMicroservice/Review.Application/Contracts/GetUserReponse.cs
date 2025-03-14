using Shared.Utilities.Response;

namespace Review.Application.Contracts
{
    public class GetUserReponse : BaseResponse
    {
        public IEnumerable<UserResponseData> Data { get; set; } = null!;
    }

    public class UserResponseData
    {
        public string UserId { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
