using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Features.User.Queries.UserAddress;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManger;

        public UserService(UserManager<ApplicationUser> userManger, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.userManger = userManger;
        }        

        public async Task<AspNetUserInfo?> GetUserInfoAsync(string id)
        {
            return await unitOfWork.UserInfoRepository.FirstOrDefaultAsync((x) => x.UserId.Equals(id) || x.UserId.Equals(id));
        }

        public async Task CreateUserInfoAsync(CreateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var userInfo = request.Adapt<AspNetUserInfo>();
            unitOfWork.UserInfoRepository.AddAsync(userInfo);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserInfoAsync(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            var userInfo = await GetUserInfoAsync(request.Id);

            userInfo!.FirstName = request.FirstName;
            userInfo.LastName = request.LastName;

            unitOfWork.UserInfoRepository.UpdateAsync(userInfo);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<AspNetUserInfo?> GetUserAvatarAsync(string id)
        {
            return await unitOfWork.UserInfoRepository.FirstOrDefaultAsync((x) => x.UserId.Equals(id) || x.UserId.Equals(id));
        }

        public async Task CreateUserAvatarAsync(CreateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var userInfo = request.Adapt<AspNetUserInfo>();
            unitOfWork.UserInfoRepository.AddAsync(userInfo);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateUserAvatarAsync(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
        {
            var entity = await GetUserAvatarAsync(request.Id);

            entity!.AvatarURI = request.AvatarURI;

            unitOfWork.UserInfoRepository.UpdateAsync(entity);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<AspNetUserAddress?> GetUserAddressById(GetUserAddressByIdQuery query, CancellationToken cancellationToken = default)
        {
            return await this.unitOfWork.AddressRepository.GetByIdAsync(query.Id);
        }

        public async Task CreateUserAddressAsnc(AspNetUserAddress request, CancellationToken cancellationToken = default)
        {
            this.unitOfWork.AddressRepository.AddAsync(request);
            await this.unitOfWork.SaveChangesAsync();
            await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now, request.Id);
        }

        public async Task UpdateUserAddressAsnc(AspNetUserAddress request, CancellationToken cancellationToken = default)
        {
            unitOfWork.AddressRepository.UpdateAsync(request);
            await unitOfWork.SaveChangesAsync(true, request.UserId, DateTime.Now, request.Id);
        }
    }
}
