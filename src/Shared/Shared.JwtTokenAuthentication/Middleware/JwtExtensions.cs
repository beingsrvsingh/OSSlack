using JwtTokenAuthentication.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.Interfaces;
using Shared.Utilities.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Shared.JwtTokenAuthentication.Middleware;
public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IPlatformService platformService)
    {        
        services.AddAuthentication()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtConstant.JWT_TOKEN_ISSUER,
                ValidAudience = JwtConstant.JWT_TOKEN_AUDIENCE,
                IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                {
                    List<SecurityKey> keys = new();

                    string? userId = GetUserIdFromToken(token);

                    if (string.IsNullOrEmpty(userId))
                        return keys;

                    var signingKey = platformService.GetCredential($"jwt:{userId}");

                    if (signingKey is null)
                        return keys;

                    var signatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                    keys.Add(signatureKey);
                    return keys;
                }
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.Response.StatusCode = 401;

                    if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Authorization))
                        context.Response.WriteAsJsonAsync(FailureResponse).Wait();
                    else
                        context.Response.WriteAsJsonAsync(FailureResponse).Wait();

                    context.HandleResponse();
                    await Task.CompletedTask;
                },
                OnAuthenticationFailed = async context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.WriteAsJsonAsync(new FailureResponse("https://tools.ietf.org/html/rfc7235#section-3.1", new { message = context.Exception.Message, reason = "forbidden" })).Wait();

                    await Task.CompletedTask;
                },
                OnTokenValidated = async context =>
                {
                    context.HttpContext.User = new GenericPrincipal(new ClaimsIdentity(context.Principal?.Claims), []);
                    await Task.CompletedTask;
                },
                OnForbidden = async context =>
                {
                    context.Response.WriteAsJsonAsync(new FailureResponse("https://tools.ietf.org/html/rfc7231#section-6.5.3", new { message = "User does not have sufficient permissions for this profile.", reason = "forbidden" })).Wait();
                    await Task.CompletedTask;
                },
                OnMessageReceived = async context =>
                {
                    await Task.CompletedTask;
                }

            };
        });

        return services;
    }

    private static string? GetUserIdFromToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(accessToken);

        string? userId = token.Claims.FirstOrDefault(claims => claims.Type == JwtConstant.JWT_TOKEN_USERID_KEYS)?.Value;

        if (string.IsNullOrEmpty(userId))
            return null;

        return userId;
    }

    private static FailureResponse FailureResponse
    {
        get
        {
            return new FailureResponse(code: "https://tools.ietf.org/html/rfc7235#section-3.1", new { message = "Token is not well formed.", reason = "Forbidden" });
        }
    }

}