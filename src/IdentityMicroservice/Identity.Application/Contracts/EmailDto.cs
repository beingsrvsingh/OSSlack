using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Contracts
{
    public class EmailDto
    {
        [EmailAddress]
        public required string Email { get; init; }
    }
}
