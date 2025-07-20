using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.SecretManager.Queries
{
    public record GetSecretQuery : IRequest<Result>
    {
        public string AppName { get; set; } = null!;
        public string Environment { get; set; } = null!;
        public string Key { get; set; } = null!;
        public GetSecretQuery(string appName, string environment, string key)
        {
            AppName = appName;
            Environment = environment;
            Key = key;
        }
    }
}
