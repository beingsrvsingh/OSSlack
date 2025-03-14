namespace Identity.Application.Contracts
{
    public record UserAvatarResponse
    {
        public required string AvatarUri { get; set; }
    };
}
