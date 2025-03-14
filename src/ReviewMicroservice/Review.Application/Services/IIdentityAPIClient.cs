using Review.Application.Contracts;

namespace Review.Application.Services
{
    public interface IIdentityAPIClient
    {
        Task<GetUserReponse?> GetUserAsync(object request);
    }
}
