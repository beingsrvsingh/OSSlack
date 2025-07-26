using Identity.Application.Features;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Features.User.Queries.UserAddress;
using Identity.Domain.Entities;

namespace Identity.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ActivateUserAsync(ActivateUserCommand request, CancellationToken cancellationToken = default);
        Task<bool> DeActivateUserAsync(DeActivateUserCommand request, CancellationToken cancellationToken);
        Task<ApplicationUser?> GetUserAvatarAsync(string userId, CancellationToken cancellationToken = default);

        Task<bool> UpdateUserInfoAsync(UpdateUserInfoCommand request, CancellationToken cancellationToken = default);

        Task<bool> UpdateUserAvatarAsync(UpdateUserAvatarCommand request, CancellationToken cancellationToken = default);

        Task<AspNetUserAddress?> GetUserAddressById(int id, CancellationToken cancellationToken = default);

        Task<bool> CreateUserAddressAsync(CreateUserAddressCommand request, CancellationToken cancellationToken = default);

        Task<bool> UpdateUserAddressAsync(UpdateUserAddressCommand request, CancellationToken cancellationToken = default);

        Task<bool> DeleteUserAddressAsync(int addressId);
    }

}
