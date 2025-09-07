using MediatR;
using Shared.Utilities.Response;
using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Application.Features.Commands
{
    public class CreateKathavachakMasterCommand : IRequest<Result>
    {
        [Required, MaxLength(36)]
        public string UserId { get; set; } = null!;

        [MaxLength(200)]
        public string? DisplayName { get; set; }

        [MaxLength(500)]
        public string? ProfilePictureUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
