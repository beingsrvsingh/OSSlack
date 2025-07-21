
namespace Shared.Domain.Enums;

public enum TokenType
{
    SigningKey,
    AccessToken,
    RefreshToken,
    VerificationToken,
    PasswordResetToken,
    EmailConfirmationToken,
    TwoFactorAuthenticationToken
}