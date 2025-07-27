using Identity.Application.Features.User.Commands.Token;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Interfaces;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class RevokeUserCommandHandler : IRequestHandler<RevokeUserCommand, Result>
    {
        private readonly ITokenService tokenService;
        private readonly ILoggerService<RevokeUserCommandHandler> loggerService;
        private readonly ICookieService cookieService;

        public RevokeUserCommandHandler(ILoggerService<RevokeUserCommandHandler> loggerService, ITokenService tokenService, ICookieService cookieService)
        {
            this.tokenService = tokenService;
            this.loggerService = loggerService;
            this.cookieService = cookieService;
        }

        public async Task<Result> Handle(RevokeUserCommand request, CancellationToken cancellationToken)
        {
            // Prefer request token, fallback to cookie
            var refreshToken = request.Token ?? cookieService.GetCookie("refreshToken");

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Result.Failure(new FailureResponse(
                    code: "InvalidRequest",
                    description: "Refresh token is missing."
                ));
            }

            var validToken = await tokenService.GetRefreshToken(refreshToken, request.UserId);

            if (validToken == null)
            {
                return Result.Failure(new FailureResponse(
                    code: "InvalidToken",
                    description: "The token is invalid, expired, or does not belong to the user."
                ));
            }

            var hasRevoked = await tokenService.RevokeTokenAsync(validToken);

            if (!hasRevoked)
            {
                return Result.Failure(new FailureResponse(
                    code: "RevokeFailed",
                    description: "Token revocation failed due to an internal error."
                ));
            }

            return Result.Success("Token revoked successfully.");
        }
    }
}
