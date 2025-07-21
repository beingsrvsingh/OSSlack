using Identity.Domain.Core.Repository;
using Identity.Domain.Core.UOW;
using Identity.Infrastructure.Persistence.Context;
using Shared.Infrastructur.UoW;

namespace Identity.Infrastructure.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<ApplicationDbContext>, IUnitOfWork
    {
        public UnitOfWork(
        ApplicationDbContext context
    ) : base(context)
        {
        }

        private IApplicationUserRepository? applicationUserRepository;
        private IUserInfoRepository? userInfoRepository;
        private IRefreshTokenRepository? refreshTokenRepository;
        private IAddressRepository? addressRepository;
        private IUserDevicesRepository? userDevicesRepository;
        private ICountryMasterRepository? countryMasterRepository;
        private IStateMasterRepository? stateMasterRepository;
        private ICityMasterRepository? areaMasterRepository;
        private IUserSigningKeyRepository? userSigningKeyRepository;

        public ICountryMasterRepository CountryMasterRepository
        {
            get
            {
                if (countryMasterRepository == null)
                {
                    countryMasterRepository = new CountryMasterRepository(_context);
                }
                return countryMasterRepository;
            }
        }

        public IStateMasterRepository StateMasterRepository
        {
            get
            {
                if (stateMasterRepository == null)
                {
                    stateMasterRepository = new StateMasterRepository(_context);
                }
                return stateMasterRepository;
            }
        }

        public ICityMasterRepository CityMasterRepository
        {
            get
            {
                if (areaMasterRepository == null)
                {
                    areaMasterRepository = new CityMasterRepository(_context);
                }
                return areaMasterRepository;
            }
        }

        public IUserInfoRepository UserInfoRepository
        {
            get
            {
                if (userInfoRepository == null)
                {
                    userInfoRepository = new UserInfoRepository(_context);
                }
                return userInfoRepository;
            }
        }

        public IApplicationUserRepository ApplicationUserRepository
        {
            get
            {
                if (applicationUserRepository == null)
                {
                    applicationUserRepository = new ApplicationUserRepository(_context);
                }
                return applicationUserRepository;
            }
        }

        public IUserDevicesRepository UserDevicesRepository
        {
            get
            {
                if (userDevicesRepository == null)
                {
                    userDevicesRepository = new UserDevicesRepository(_context);
                }
                return userDevicesRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (refreshTokenRepository == null)
                {
                    refreshTokenRepository = new RefreshTokenRepository(_context);
                }
                return refreshTokenRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                if (addressRepository is null)
                {
                    addressRepository = new AddressRepository(_context);
                }
                return addressRepository;
            }
        }

        public IUserSigningKeyRepository UserSigningKeyRepository
        {
            get
            {
                if (userSigningKeyRepository is null)
                {
                    userSigningKeyRepository = new UserSigningKeyRepository(_context);
                }
                return userSigningKeyRepository;
            }
        }
    }
}