using Utilities.Cryptography;

namespace JwtTokenAuthentication
{
    public static class SecurityTokenExtensions
    {
        public static string GenerateSecurityKey(string key)
        {
            return Cryptography.EncryptString(key);
        }
    }
}
