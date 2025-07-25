namespace Identity.Application.Services.Interfaces
{
    public interface ISeedService
    {
        Task<bool> CreateRoleSync();
    }
}
