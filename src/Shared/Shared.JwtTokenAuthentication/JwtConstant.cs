namespace JwtTokenAuthentication.Constants
{
    public static class JwtConstant
    {
        public static readonly string JWT_TOKEN_ISSUER = "https://osslack:80";
        public static readonly string JWT_TOKEN_AUDIENCE = "https://osslack:80";
        public static readonly string JWT_TOKEN_SECURITYKEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJc3N1ZXIgKGlzcykiOiJJc3N1ZXIiLCJJc3N1ZWQgQXQgKGlhdCkiOiIyMDI07";
        public static readonly DateTime JWT_TOKEN_EXPIRATION = DateTime.UtcNow.AddDays(7);
        public static readonly DateTime JWT_REFRESH_TOKEN_EXPIRATION = DateTime.UtcNow.AddHours(1);
        public static readonly string JWT_TOKEN_ROLE_KEYS = "role";
        public static readonly string JWT_TOKEN_DEFAULT_ROLE = "Customer";
        public static readonly string JWT_TOKEN_USERID_KEYS = "id";
        public static readonly string JWT_TOKEN_USERNAME_KEYS = "username";
        public static readonly string JWT_TOKEN_PHONENUMBER_KEYS = "phonenumber";
        public static readonly string JWT_TOKEN_SCOPE_KEYS = "scope";
        public static readonly string JWT_TOKEN_SCOPE = "access_as_user";
        public static readonly TimeSpan JWT_TOKEN_LIFETIME = TimeSpan.FromHours(1);

    }
}
