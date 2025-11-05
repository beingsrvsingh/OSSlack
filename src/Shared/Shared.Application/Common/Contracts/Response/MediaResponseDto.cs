namespace Shared.Application.Common.Contracts.Response
{
    public class MediaResponseDto
    {
        // Image, Video
        public string Type { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? AltText { get; set; }
        public int SortOrder { get; set; }
    }
}
