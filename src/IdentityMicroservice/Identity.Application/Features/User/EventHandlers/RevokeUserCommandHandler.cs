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
            var refreshToken = request.Token ?? cookieService.GetCookie("refreshToken");

            var validToken = await tokenService.GetRefreshToken(refreshToken, request.UserId);

            if (validToken == null)
            {
                return Result.Failure(new FailureResponse(
                    code: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    description: "Token is invalid or malformed."));
            }

            await tokenService.RevokeTokenAsync(validToken);

            return Result.Success("Token revoked successfully.");
        }
    }
}
