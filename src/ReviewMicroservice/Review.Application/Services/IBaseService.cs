namespace Review.Application.Services
{
    public interface IBaseService<T> where T : class
    {        
        Task AddAsync(T entities);

        Task UpdateAsync(T entities);
    }
}
