using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Entities;
using SecretManagement.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;


namespace SecretManagement.Infrastructure.Repositories;

public class ApiKeyRepository : Repository<ApiSecret>, IApiKeyRepository
{
    private readonly new SecretManagementDbContext _dbContext;

    public ApiKeyRepository(SecretManagementDbContext dbContext) : base(dbContext)
    {
        this._dbContext = dbContext;
    }

}