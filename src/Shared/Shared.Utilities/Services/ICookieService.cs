namespace Utilities.Services
{
    public interface ICookieService
    {
        string GetCookie(string key);
        void SetCookie(string key, DateTime expiresIn);
        void DeleteCookie(string key);
        void RemoveAndSetCookie(string key, DateTime expiresIn);
    }
}
