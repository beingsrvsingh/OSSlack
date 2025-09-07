using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Queries
{
    public record GetTempleDonationByIdQuery(int Id) : IRequest<Result>;

}
