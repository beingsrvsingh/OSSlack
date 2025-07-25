namespace Identity.Application.Contracts
{
    public class UpdateUserAvatarRequest
    {
        public required string AvatarURI { get; set; }
        public required string Avatar { get; set; }
    }
}
