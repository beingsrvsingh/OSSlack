using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Domain;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class CreateUserEmailCommandHandler : IRequestHandler<CreateUserEmailCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly ILoggerService<CreateUserEmailCommandHandler> _logger;
    private readonly IUnitOfWork unitOfWork;
    private readonly IFirebaseAuthService _firebaseAuthService;

    public CreateUserEmailCommandHandler(ILoggerService<CreateUserEmailCommandHandler> loggerService, IUnitOfWork unitOfWork,
    IIdentityService identityService, IFirebaseAuthService firebaseAuthService)
    {
        this.identityService = identityService;
        this._logger = loggerService;
        this.unitOfWork = unitOfWork;
        this._firebaseAuthService = firebaseAuthService;
    }

    public async Task<Result> Handle(CreateUserEmailCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInfo("Register request received for email: {0}", request.Email);

        // var payload = await _firebaseAuthService.VerifyTokenAndGetPayloadAsync(request.FirebaseIdToken);
        // if (payload == null)
        // {
        //     _logger.LogWarning("Firebase token validation failed during registration.");
        //     return Result.Failure(new FailureResponse("InvalidFirebaseToken", "Firebase token validation failed."));
        // }

        // var firebaseUserId = payload.Subject;
        // payload.Claims.TryGetValue("email", out var emailObj);
        // var emailFromToken = emailObj?.ToString();

        // if (string.IsNullOrWhiteSpace(emailFromToken) ||
        //     !string.Equals(emailFromToken, request.Email.ToString(), StringComparison.OrdinalIgnoreCase))
        // {
        //     _logger.LogWarning("Email from Firebase token does not match the requested email during registration.");
        //     return Result.Failure(new FailureResponse("EmailIdMismatch", "Email in token does not match the requested email."));
        // }

        //request.FirebaseIdToken = firebaseUserId;

        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await identityService.CreateUserEmailAsync(request, cancellationToken);

            if (!result.Succeeded)
            {
                _logger.LogWarning("User registration failed for email: {0}. Errors: {1}",
                    request.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                return Result.Failure(result.Errors);
            }

            var user = await identityService.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogError("User registration succeeded, but user retrieval failed for email: {0}", request.Email);
                return Result.Failure(new FailureResponse("UserRetrievalFailed", "Unable to retrieve user after registration."));
            }

            await identityService.CreateUserRoleAsync(user.Id, Roles.Customer);
            await identityService.AddUserDeviceAsync(user.Id, cancellationToken);

            _logger.LogInfo("User {0} registered and assigned role {1}", user.Id, Roles.Customer);

            var token = await identityService.GenerateTokenAsync(user.Id);

            if (token == null)
            {
                _logger.LogError("Token generation failed for new user {0}", user.Id);
                return Result.Failure(new FailureResponse("TokenGenerationFailed", "Failed to generate authentication token."));
            }

            await unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInfo("Token successfully generated for new user {0}", user.Id);

            return Result.Success(token);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Error occurred during email registration. Transaction rolled back.");
            return Result.Failure(new FailureResponse("RegistrationFailed", "An error occurred during email registration."));
        }
    }
}