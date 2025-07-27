using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Identity.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Application.Interfaces.Platform;
using Shared.Domain.Contracts;
using Shared.Utilities;

namespace Identity.Infrastructure.Services.Identity;

public class FirebaseAuthService : IFirebaseAuthService
{
    private readonly ILoggerService<FirebaseAuthService> _logger;
    private readonly IJsonFileService _jsonFileService;
    private FirebaseAuth? _firebaseAuth;

    public FirebaseAuthService(
        ILoggerService<FirebaseAuthService> logger,
        IJsonFileService jsonFileService)
    {
        _logger = logger;
        _jsonFileService = jsonFileService;
    }

    public async Task<FirebaseTokenPayload?> VerifyTokenAndGetPayloadAsync(string firebaseIdToken)
    {
        try
        {
            await EnsureFirebaseInitializedAsync();

            var decodedToken = await _firebaseAuth!.VerifyIdTokenAsync(firebaseIdToken);
            if (decodedToken == null)
                return null;

            return new FirebaseTokenPayload
            {
                Subject = decodedToken.Uid,
                Claims = decodedToken.Claims.ToDictionary(
                    kv => kv.Key,
                    kv => kv.Value?.ToString() ?? string.Empty)
            };
        }
        catch (FirebaseAuthException ex)
        {
            _logger.LogError(ex, "Firebase token verification failed: {Message}", ex.Message);
            return null;
        }
    }
    
    private async Task EnsureFirebaseInitializedAsync()
    {
        if (_firebaseAuth != null)
            return;

        if (FirebaseApp.DefaultInstance == null)
        {
            var fileName = Configuration.GetValue<string>("FirebaseConfig", "FileName");
            var envFileName = $"{fileName}.{Configuration.GetEnvironmentVariable}";
            var configPath = Configuration.GetPath(envFileName);
            var json = await _jsonFileService.ReadAsync(configPath);

            if (string.IsNullOrWhiteSpace(json))
            {
                _logger.LogError("Missing Firebase config at path {Path}", configPath);
                throw new InvalidOperationException("Missing Firebase configuration");
            }

            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromJson(json)
            });

            _logger.LogInfo("FirebaseApp initialized successfully.");
        }

        _firebaseAuth = FirebaseAuth.DefaultInstance;
    }
}
