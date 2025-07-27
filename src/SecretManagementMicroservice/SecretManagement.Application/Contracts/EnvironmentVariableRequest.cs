
namespace SecretManagement.Application.Contracts
{
    public class EnvironmentVariableRequest
    {
        public string Key { get; set; } = null!;
        public string? Value { get; set; }
    }
}