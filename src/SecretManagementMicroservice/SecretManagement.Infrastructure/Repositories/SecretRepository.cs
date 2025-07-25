using Microsoft.EntityFrameworkCore;
using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Entities;
using SecretManagement.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;


namespace SecretManagement.Infrastructure.Repositories;

public class SecretRepository : Repository<Secret>, ISecretRepository
{
    private readonly new SecretManagementDbContext _dbContext;

    public SecretRepository(SecretManagementDbContext dbContext) : base(dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Dictionary<string, Secret>> GetAllAsync(string userId)
    {
        return await _dbContext.Secrets
            .Where(s => s.UserId == userId && s.IsActive)
            .ToDictionaryAsync(s => s.SecretKey, s => s);
    }

}