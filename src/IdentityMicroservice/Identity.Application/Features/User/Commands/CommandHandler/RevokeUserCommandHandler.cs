using Identity.Application.Features.User.Commands.Token;
using MediatR;
using Utilities.Services;
using Shared.Application.Common.Services.Interfaces;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class RevokeUserCommandHandler : IRequestHandler<RevokeUserCommand, Result>
    {
        private readonly ITokenService tokenService;
        private readonly ILoggerService loggerService;
        private readonly ICookieService cookieService;

        public RevokeUserCommandHandler(ITokenService tokenService, ILoggerService loggerService, ICookieService cookieService)
        {
            this.tokenService = tokenService;
            this.loggerService = loggerService;
            this.cookieService = cookieService;
        }

        public async Task<Result> Handle(RevokeUserCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = request.Token ?? cookieService.GetCookie("refreshToken");

            var validToken =await tokenService.GetRefreshToken(refreshToken, request.UserId);

            if (validToken is null)
                return Result.Failure(new FailureResponse(code: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1", description: "Token is malfuntioned."));

            await tokenService.RevokeTokenAsync(validToken);

            return Result.Success();
        }
    }
}
