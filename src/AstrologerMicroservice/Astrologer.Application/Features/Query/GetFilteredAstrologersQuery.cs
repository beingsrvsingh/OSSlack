using System.Text.Json.Serialization;
using MediatR;
using Shared.Utilities.Response;

namespace Astrologer.Application.Features.Query
{
    public class GetFilteredAstrologersQuery : IRequest<Result>
    {
        [JsonPropertyName("attr_id")]
        public required List<int> AttributeId { get; set; }
        [JsonPropertyName("cid")]
        public string CategoryId { get; set; } = null!;
        [JsonPropertyName("scid")]
        public string SubCategoryId { get; set; } = null!;
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; } = 1;
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; } = 20;
        [JsonPropertyName("sort_by")]
        public string? SortBy { get; set; }
        [JsonPropertyName("sort_desc")]
        public bool SortDescending { get; set; } = false;
    }


}