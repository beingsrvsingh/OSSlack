using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands.CommandsHandler
{
    public class RoleCommandHandler : IRequestHandler<RoleCommand, Result>, IRequestHandler<UserRoleCommand, Result>
    {
        private readonly IRoleService roleService;
        private readonly ILoggerService loggerService;

        public RoleCommandHandler(IRoleService roleService, ILoggerService loggerService)
        {
            this.roleService = roleService;
            this.loggerService = loggerService;
        }

        public async Task<Result> Handle(RoleCommand command, CancellationToken cancellationToken)
        {
            await this.roleService.AddRoles(command);
            return Result.Success();
        }

        public async Task<Result> Handle(UserRoleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await this.roleService.AddUserRoles(command);
                return Result.Success();
            }
            catch (Exception ex)
            {
                this.loggerService.LogError(ex);
                return Result.Failure();
            }            
        }
    }
}
