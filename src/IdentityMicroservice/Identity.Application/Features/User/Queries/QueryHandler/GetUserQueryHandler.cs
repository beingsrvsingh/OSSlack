using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Queries.QueryHandler
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result>
    {
        private readonly IIdentityService identityService;

        public GetUserQueryHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<Result> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var users = await this.identityService.GetUserAsync(request.UserId);

            if (users is null)
                return Result.Failure("No Records Found");

            return Result.Success(users);
        }
    }
}
