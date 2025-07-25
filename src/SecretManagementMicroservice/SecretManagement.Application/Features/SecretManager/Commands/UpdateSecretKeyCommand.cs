using Shared.Utilities.Response;
using MediatR;
using Shared.Utilities;

namespace SecretManagement.Application.Features.SecretManager.Commands
{
    public class UpdateSecretKeyCommand : IRequest<Result>
    {
        private string _secretValue = string.Empty;
        public string AppName { get; set; } = null!;
        public string Environment { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public string SecretValue
        {
            get => _secretValue;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Secret value cannot be null or empty.", nameof(value));

                if (!Utils.IsValidJson(value))
                    throw new ArgumentException("Secret value must be valid JSON.", nameof(value));

                _secretValue = value;
            }
        }

        public DateTime? ExpiryDate { get; set; }       
        public string? Description { get; set; } 
    }
}
