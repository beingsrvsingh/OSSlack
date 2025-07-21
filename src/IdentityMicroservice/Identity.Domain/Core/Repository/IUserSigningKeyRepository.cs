using JwtTokenAuthentication.Domain.Entities;

namespace Identity.Domain.Core.Repository
{
    public interface IUserSigningKeyRepository : IRepository<AspNetUserSigningKey> { }
}
