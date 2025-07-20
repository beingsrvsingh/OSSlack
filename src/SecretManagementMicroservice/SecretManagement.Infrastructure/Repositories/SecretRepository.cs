using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Entities;
using SecretManagement.Infrastructure.Persistence.Context;
using Shared.Infrastructure.Repositories;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;


namespace SecretManagement.Infrastructure.Repositories;

public class SecretRepository : Repository<Secret>, ISecretRepository
{
    private readonly new SecretManagementDbContext _dbContext;

    public SecretRepository(SecretManagementDbContext dbContext) : base(dbContext)
    {
        this._dbContext = dbContext;
    }

    public Dictionary<string, Secret> GetAll(string appName, string environment)
    {
        return _dbContext.Secrets
            .Where(s => s.AppName == appName && s.Environment == environment && s.IsActive)
            .ToDictionary(s => s.SecretKey, s => s);
    }

}