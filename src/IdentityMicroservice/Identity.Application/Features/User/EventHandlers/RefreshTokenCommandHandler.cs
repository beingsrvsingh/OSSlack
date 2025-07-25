using Identity.Application.Features.User.Commands.Token;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Interfaces;
using Shared.Utilities;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result>
{
    private readonly ITokenService tokenService;
    private readonly ILoggerService<RefreshTokenCommandHandler> loggerService;
    private readonly IHttpRequestService securityService;
    private readonly ICookieService cookieService;

    public RefreshTokenCommandHandler(ILoggerService<RefreshTokenCommandHandler> loggerService, ITokenService tokenService,
        IHttpRequestService securityService, ICookieService cookieService)
    {
        this.tokenService = tokenService;
        this.loggerService = loggerService;
        this.securityService = securityService;
        this.cookieService = cookieService;
    }

    public async Task<Result> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var response = await tokenService.GenerateRefreshTokenAsync(
            request.UserId,
            request.RefreshToken,
            securityService.GetIpAddress);

        if (response != null)
        {
            // Set Refresh token in cookie to validate from cookie.
            cookieService.RemoveAndSetCookie(response.RefreshToken, Constants.DEFAULT_COOKIE_PERIOD);

            return Result.Success(response);
        }

        return Result.Failure(new FailureResponse(
            code: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            description: "Refresh token is invalid."));
    }

}