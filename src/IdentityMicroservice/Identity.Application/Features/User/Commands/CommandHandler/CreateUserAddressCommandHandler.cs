using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Domain.Entities;
using Mapster;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, Result>
    {
        private readonly IUserService userService;

        public CreateUserAddressCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Result> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var address = request.Adapt<AspNetUserAddress>();
            await this.userService.CreateUserAddressAsnc(address);

            return Result.Success();
        }
    }
}
