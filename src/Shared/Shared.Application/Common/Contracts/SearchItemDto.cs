using Shared.Application.Common.Contracts;
using Shared.Domain.Entities.Base;

namespace Shared.Application.Contracts
{
    public class SearchItemDto : BaseProductResponseDto
    {
        public SearchItemMeta SearchItemMeta { get; set; } = new();
        public List<BaseAttributeValue>? AttributeValues { get; set; } = [];
    }

    public class SearchItemMeta
    {
        public float Score { get; set; }
        public string MatchType { get; set; } = "Partial";
    }
}
