using Identity.Application.Features;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Shared.Application.Interfaces.Logging;

namespace Identity.Infrastructure.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILoggerService<UserService> _logger;
        private readonly UserManager<ApplicationUser> userManger;
        private readonly IIdentityService identityService;

        public UserService(ILoggerService<UserService> logger, UserManager<ApplicationUser> userManger, IIdentityService identityService, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this._logger = logger;
            this.userManger = userManger;
            this.identityService = identityService;
        }

        public async Task<bool> UpdateUserInfoAsync(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInfo = await identityService.GetUserByIdAsync(request.UserId);

                if (userInfo == null)
                {
                    _logger.LogWarning("User not found with ID: {UserId}", request.UserId);
                    return false;
                }

                userInfo.Adapt(userInfo);
                await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now, Convert.ToInt32(request.Id));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user info with ID: {UserId}", request.Id);
                return false;
            }
        }

        public async Task<bool> ActivateUserAsync(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInfo = await identityService.GetUserByIdAsync(request.UserId);

                if (userInfo == null)
                {
                    _logger.LogWarning("User not found with UserId: {UserId}", request.UserId);
                    return false;
                }

                userInfo.Adapt(userInfo);
                await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user info with UserId: {UserId}", request.UserId);
                return false;
            }
        }

        public async Task<bool> DeActivateUserAsync(DeActivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userInfo = await identityService.GetUserByIdAsync(request.UserId);

                if (userInfo == null)
                {
                    _logger.LogWarning("User not found with UserId: {UserId}", request.UserId);
                    return false;
                }

                userInfo.Adapt(userInfo);
                await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user info with Email: {UserId}", request.UserId);
                return false;
            }
        }

        public async Task<ApplicationUser?> GetUserAvatarAsync(string userId, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.UserInfoRepository.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task<bool> UpdateUserAvatarAsync(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await GetUserAvatarAsync(request.UserId);
                if (entity == null)
                {
                    _logger.LogWarning("User not found for avatar update with ID: {UserId}", request.UserId);
                    return false;
                }

                entity.ProfilePictureUrl = request.AvatarURI;

                await unitOfWork.UserInfoRepository.UpdateAsync(entity);
                await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user avatar with ID: {UserId}", request.UserId);
                return false;
            }
        }

        public async Task<AspNetUserAddress?> GetUserAddressById(int id, CancellationToken cancellationToken = default)
        {
            return await unitOfWork.AddressRepository.GetByIdAsync(id);
        }

        public async Task<bool> CreateUserAddressAsync(CreateUserAddressCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var userAddress = request.Adapt<AspNetUserAddress>();
                await unitOfWork.AddressRepository.AddAsync(userAddress);
                await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now, userAddress.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user address for UserId: {UserId}", request.UserId);
                return false;
            }
        }

        public async Task<bool> UpdateUserAddressAsync(UpdateUserAddressCommand request, CancellationToken cancellationToken = default)
        {
            try
            {
                var adddress = await GetUserAddressById(Convert.ToInt32(request.Id));
                if (adddress == null)
                    return false;

                request.Adapt(adddress);

                await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now, Convert.ToInt32(request.Id));
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user address for UserId: {UserId}", request.UserId);
                return false;
            }
        }

        public async Task<bool> DeleteUserAddressAsync(int addressId)
        {
            var address = await unitOfWork.AddressRepository.GetByIdAsync(addressId);

            if (address == null)
                return false;

            await unitOfWork.AddressRepository.DeleteAsync(address);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

    }
}
