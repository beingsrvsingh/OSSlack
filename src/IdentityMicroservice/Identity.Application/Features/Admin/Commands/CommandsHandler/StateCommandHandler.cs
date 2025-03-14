using Identity.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class StateCommandHandler : IRequestHandler<StateCommand, Result>
    {
        private readonly IAdminService service;
        private readonly IWebHostEnvironment env;

        public StateCommandHandler(IAdminService service, IWebHostEnvironment env)
        {
            this.service = service;
            this.env = env;
        }

        Task<Result> IRequestHandler<StateCommand, Result>.Handle(StateCommand command, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success());
        }
    }
}
