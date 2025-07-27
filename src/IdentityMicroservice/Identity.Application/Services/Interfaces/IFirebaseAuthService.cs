using Shared.Domain.Contracts;

namespace Identity.Application.Services.Interfaces;

public interface IFirebaseAuthService
{
    /// <summary>
    /// Verifies Firebase ID token and returns the decoded payload with claims.
    /// Returns null if token is invalid.
    /// </summary>
    /// <param name="firebaseIdToken">Firebase JWT token</param>
    /// <returns>Decoded Firebase token payload or null if invalid</returns>
    Task<FirebaseTokenPayload?> VerifyTokenAndGetPayloadAsync(string firebaseIdToken);

}
