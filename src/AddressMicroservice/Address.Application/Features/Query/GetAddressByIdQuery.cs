using Address.Application.Contracts;
using MediatR;
using Shared.Utilities.Response;

namespace Address.Application.Features.Query
{
    public class GetAddressByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
    }
}