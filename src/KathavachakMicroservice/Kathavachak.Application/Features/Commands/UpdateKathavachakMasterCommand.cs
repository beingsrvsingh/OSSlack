using MediatR;
using Shared.Utilities.Response;
using System.ComponentModel.DataAnnotations;

namespace Kathavachak.Application.Features.Commands
{
    public class UpdateKathavachakMasterCommand : IRequest<Result>
    {
        [Required]
        public int Id { get; set; }

        [MaxLength(200)]
        public string? DisplayName { get; set; }

        [MaxLength(500)]
        public string? ProfilePictureUrl { get; set; }

        public bool IsActive { get; set; }
    }

}
