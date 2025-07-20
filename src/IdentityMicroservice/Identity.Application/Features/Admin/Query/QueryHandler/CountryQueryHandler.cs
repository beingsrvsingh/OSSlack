using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class CountryQueryHandler : IRequestHandler<GetAllCountryQuery, Result>
    {
        public CountryQueryHandler()
        {
            
        }
        public Task<Result> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
