using Identity.Domain.Core.Repository;
using Shared.Domain.Common.Entities.Interface;
using Shared.Domain.UOW;

namespace Identity.Domain.Core.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IApplicationUserRepository ApplicationUserRepository { get; }
        IUserInfoRepository UserInfoRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }

        IAddressRepository AddressRepository { get; }

        IUserDevicesRepository UserDevicesRepository { get; }

        ICountryMasterRepository CountryMasterRepository { get; }

        IStateMasterRepository StateMasterRepository { get; }

        ICityMasterRepository CityMasterRepository { get; }
    }
}
