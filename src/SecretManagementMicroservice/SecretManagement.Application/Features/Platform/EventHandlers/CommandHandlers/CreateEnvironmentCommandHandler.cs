
using MediatR;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventsHandler.CommandHandler;
public class CreateEnvironmentCommandHandler : IRequestHandler<CreateEnvironmentCommand, Result>
{
    private readonly IPlatformManagerService _platformService;

    public CreateEnvironmentCommandHandler(IPlatformManagerService platformService)
    {
        _platformService = platformService;
    }

    public Task<Result> Handle(CreateEnvironmentCommand request, CancellationToken cancellationToken)
    {
        _platformService.AddCredential(request.EnvironmentKey, request.EnvironmentValue);
        return Task.FromResult(Result.Success());
    }
}