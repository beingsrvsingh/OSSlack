namespace Identity.Application.Services.Interfaces
{
    public interface IIdentityRole
    {
        public static string[] roles() => new string[] { "Superuser", "Admin", "Customer", "Seller" };
    }
}
