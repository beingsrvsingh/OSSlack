using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class CountryQueryHandler : IRequestHandler<GetAllCountryQuery, IResult>
    {
        public CountryQueryHandler()
        {
            
        }
        public Task<IResult> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
