using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Domain.Entities;
using Mapster;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, Result>
    {
        private readonly IUserService userService;

        public UpdateUserAddressCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var address = request.Adapt<AspNetUserAddress>();
            await this.userService.UpdateUserAddressAsnc(address);

            return Result.Success(address);
        }
    }
}
