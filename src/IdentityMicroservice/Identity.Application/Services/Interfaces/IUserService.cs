using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Features.User.Queries.UserAddress;
using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<AspNetUserInfo?> GetUserInfoAsync(string id);

        Task<AspNetUserInfo?> GetUserAvatarAsync(string id);

        Task CreateUserInfoAsync(CreateUserInfoCommand request, CancellationToken cancellationToken = default);

        Task UpdateUserInfoAsync(UpdateUserInfoCommand request, CancellationToken cancellationToken = default);

        Task CreateUserAvatarAsync(CreateUserAvatarCommand request, CancellationToken cancellationToken = default);

        Task UpdateUserAvatarAsync(UpdateUserAvatarCommand request, CancellationToken cancellationToken = default);

        Task<AspNetUserAddress?> GetUserAddressById(GetUserAddressByIdQuery query, CancellationToken cancellationToken = default);

        Task CreateUserAddressAsnc(AspNetUserAddress request, CancellationToken cancellationToken = default);

        Task UpdateUserAddressAsnc(AspNetUserAddress request, CancellationToken cancellationToken = default);
    }

}
