using Review.Application.Contracts;

namespace Review.Application.Services
{
    public interface IIdentityApiClient
    {
        Task<GetUserReponse?> GetUserAsync(object request);
    }
}
