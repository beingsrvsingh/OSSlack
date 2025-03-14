using MediatR;
using Newtonsoft.Json;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class CityCommand : IRequest<Result>
    {
        public string CircleName { get; set; } = null!;
        public string RegionName { get; set; } = null!;
        public string DivisionName { get; set; } = null!;        
        public string OfficeName { get; set; } = null!;
        public int Pincode { get; set; }
        public string OfficeType { get; set; } = null!;
        public string Delivery { get; set; } = null!;
        public string District { get; set; } = null!;
        public string StateName { get; set; } = null!;
    }
}
