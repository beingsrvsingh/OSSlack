using MediatR;
using Shared.Utilities.Response;
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Features.Admin.Commands
{
    public class VerificationAstrologerCommand : IRequest<Result>
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; set; }
    }
}
