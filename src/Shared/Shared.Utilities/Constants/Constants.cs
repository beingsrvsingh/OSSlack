namespace Shared.Utilities.Constants
{
    public static class Constants
    {
        public static readonly DateTime DEFAULT_COOKIE_PERIOD = DateTime.Now.AddDays(365);
        public static readonly string STATIC_FILE_PATH = "StaticFiles";
        public static string LoggerBaseApiGatewayUri => "https://localhost:7075";
    }
}
