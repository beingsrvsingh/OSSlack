using JwtTokenAuthentication.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Contracts.Interfaces;
using Shared.Utilities;
using Shared.Utilities.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Shared.JwtTokenAuthentication.Middleware;

public static class JwtExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        // Register authentication services first
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        // Register post-configuration for JwtBearerOptions using a factory
        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IServiceProvider>((options, serviceProvider) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = JwtConstant.JWT_TOKEN_ISSUER,
                    ValidAudience = JwtConstant.JWT_TOKEN_AUDIENCE,
                    ValidateIssuer = true,
                    ValidateAudience = true,

                    IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                    {
                        var keys = new List<SecurityKey>();

                        var userId = GetUserIdFromToken(token);
                        if (string.IsNullOrEmpty(userId)) return keys;

                        var resolver = serviceProvider.GetRequiredService<ISigningKeyProvider>();

                        var response = resolver.GetSigningKey();

                        if (string.IsNullOrEmpty(response))
                                throw new InvalidOperationException("Signing key retrieval failed: the key data is missing or invalid.");

                        var result = JsonSerializerWrapper.Deserialize<Result>(response)
                            ?? throw new InvalidOperationException("Failed to deserialize signing key result.");

                        var signingKey = result.Data?.ToString();
                        if (string.IsNullOrWhiteSpace(signingKey))
                            throw new InvalidOperationException("Signing key is missing or empty.");

                        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
                        keys.Add(securityKey);

                        return keys;
                    }
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.Response.StatusCode = 401;
                        context.HandleResponse();
                        await context.Response.WriteAsJsonAsync(FailureResponse);
                    },
                    OnAuthenticationFailed = async context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            await context.Response.WriteAsJsonAsync(new FailureResponse(
                                "https://tools.ietf.org/html/rfc7235#section-3.1",
                                new { message = context.Exception.Message, reason = "Token expired" }));
                        }
                    },
                    OnTokenValidated = async context =>
                    {
                        context.HttpContext.User = new GenericPrincipal(new ClaimsIdentity(context.Principal?.Claims), []);
                        await Task.CompletedTask;
                    },
                    OnForbidden = async context =>
                    {
                        await context.Response.WriteAsJsonAsync(new FailureResponse(
                            "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                            new { message = "User does not have sufficient permissions.", reason = "forbidden" }));
                    },
                    OnMessageReceived = _ => Task.CompletedTask
                };
            });

        return services;
    }

    private static string? GetUserIdFromToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(accessToken);
        return token.Claims.FirstOrDefault(c => c.Type == JwtConstant.JWT_TOKEN_USERID_KEYS)?.Value;
    }

    private static FailureResponse FailureResponse =>
        new("https://tools.ietf.org/html/rfc7235#section-3.1", new { message = "Token is not well formed.", reason = "Forbidden" });
}
