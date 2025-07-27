
namespace Shared.Domain.Contracts
{
    public class FirebaseTokenPayload
    {
        public string Subject { get; set; } = string.Empty;
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
    }
}
