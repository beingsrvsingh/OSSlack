namespace Review.Application.Services
{
    public interface IBaseService<T> where T : class
    {        
        void AddAsync(T entities);

        void UpdateAsync(T entities);
    }
}
