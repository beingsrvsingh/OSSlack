
using SecretManagement.Application.Services.Interfaces;

namespace Shared.Infrastructure.Extensions;
public static class FirebaseEnvExtensions
{
    public static string GetFirebaseCredentials(this IEnvironmentService env)
    {
        return env.GetVariable("FIREBASE_CREDENTIAL_JSON")
            ?? throw new InvalidOperationException("Firebase credential not set.");
    }
}
