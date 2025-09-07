using MediatR;
using Shared.Utilities.Response;
using Temple.Application.Contracts;

namespace Temple.Application.Features.Commands
{
    public record UpdateTempleDonationCommand(int Id, TempleDonationDto DonationDto) : IRequest<Result>;

}
