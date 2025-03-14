using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Utilities
{
    public class Utitlities
    {
        public static byte[] GenerateRandomNumber(string keys)
        {
            var randomNumber = new byte[keys.Length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static string GetToken(HttpContext context)
        {
            return context?.Request.Headers["Authorization"]!;
        }

        public static string GetUserIdFromToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            return token.Claims.FirstOrDefault(claims => claims.Type == "Id")!.Value;
        }

        public static string GetRoleFromToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);
            return token.Claims.FirstOrDefault(claims => claims.Type == ClaimTypes.Role)!.Value;
        }

        public static HttpClient AddHeadersToken(HttpClient httpClient, HttpContext? httpContext)
        {
            var authHeader = GetSchemeAndParametersFromToken(httpContext);

            if (authHeader.Scheme is null || authHeader.Parameter is null)
                throw new ArgumentNullException("Authorization is null");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authHeader.Scheme, authHeader.Parameter);

            return httpClient;
        }

        public static HttpClient AddHeadersToken(HttpClient httpClient, string scheme, string parameter)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter);

            return httpClient;
        }

        public static (string? Scheme, string? Parameter) GetSchemeAndParametersFromToken(HttpContext? httpContext)
        {
            string token = GetToken(httpContext!);

            if (token is null)
                return (null, null);

            var authHeader = AuthenticationHeaderValue.Parse(token);

            if (authHeader.Scheme is null || authHeader.Parameter is null)
                return (null, null);

            return (authHeader.Scheme, authHeader.Parameter);
        }

        public async static Task<HttpContext> TokenMissingException(HttpContext context)
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Code = "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1", Description = new string[] { "Token is missing." } }));
            return context;
        }
    }
}
