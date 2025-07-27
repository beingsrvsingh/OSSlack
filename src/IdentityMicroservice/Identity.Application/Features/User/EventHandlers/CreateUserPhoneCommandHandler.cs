using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Services.Interfaces;
using Identity.Domain.Core.UOW;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Domain;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler;

public class CreateUserPhoneCommandHandler : IRequestHandler<CreateUserPhoneCommand, Result>
{
    private readonly IIdentityService identityService;
    private readonly IFirebaseAuthService _firebaseAuthService;
    private readonly ILoggerService<CreateUserPhoneCommandHandler> _logger;
    private readonly IUnitOfWork unitOfWork;

    public CreateUserPhoneCommandHandler(ILoggerService<CreateUserPhoneCommandHandler> loggerService,
     IUnitOfWork unitOfWork, IIdentityService identityService, IFirebaseAuthService firebaseAuthService)
    {
        this.identityService = identityService;
        this._firebaseAuthService = firebaseAuthService;
        this._logger = loggerService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateUserPhoneCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInfo("Phone registration request received for phone: {0}", request.PhoneNumber);

        // var payload = await _firebaseAuthService.VerifyTokenAndGetPayloadAsync(request.FirebaseIdToken);
        // if (payload == null)
        // {
        //     _logger.LogWarning("Firebase token validation failed during phone registration.");
        //     return Result.Failure(new FailureResponse("InvalidFirebaseToken", "Firebase token validation failed."));
        // }

        // var firebaseUserId = payload.Subject;
        // payload.Claims.TryGetValue("phone_number", out var phoneObj);
        // var phoneFromToken = phoneObj?.ToString();

        // if (string.IsNullOrWhiteSpace(phoneFromToken) ||
        //     !string.Equals(phoneFromToken, request.PhoneNumber.ToString(), StringComparison.OrdinalIgnoreCase))
        // {
        //     _logger.LogWarning("Phone number from Firebase token does not match the requested phone number.");
        //     return Result.Failure(new FailureResponse("PhoneNumberMismatch", "Phone number in token does not match the requested phone number."));
        // }

        // request.FirebaseIdToken = firebaseUserId;

        using var transaction = await unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var userId = await identityService.CreateUserPhoneAsync(request, cancellationToken);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogError("Failed to create user with phone number {0}.", request.PhoneNumber);
                return Result.Failure(new FailureResponse(
                    "UserCreationFailed",
                    "Failed to create user with the provided phone information."));
            }

            var hasRoleCreated = await identityService.CreateUserRoleAsync(userId, Roles.Customer);
            if (!hasRoleCreated)
            {
                _logger.LogWarning("Failed to assign Customer role to user {UserId}.", userId);
            }

            await identityService.AddUserDeviceAsync(userId, cancellationToken);

            _logger.LogInfo("User with ID {UserId} created successfully via phone.", userId);

            // 🔑 Step 5: Token Generation
            var token = await identityService.GenerateTokenAsync(userId);

            if (token == null)
            {
                _logger.LogError("Failed to generate token for user {UserId}.", userId);
                return Result.Failure(new FailureResponse("TokenGenerationFailed", "Failed to generate authentication token."));
            }

            await unitOfWork.CommitTransactionAsync(cancellationToken);
            _logger.LogInfo("Token generated successfully for phone user {UserId}.", userId);

            return Result.Success(token);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync(cancellationToken);
            _logger.LogError(ex, "Error occurred during phone registration. Transaction rolled back.");
            return Result.Failure(new FailureResponse("RegistrationFailed", "An error occurred during phone registration."));
        }
    }
}