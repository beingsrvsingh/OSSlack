using Shared.Application.Common.Contracts;
using Shared.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Shared.Application.Contracts
{
    public class SearchResponseDto : BaseProductResponseDto
    {
        [JsonPropertyName("attributes")]
        public List<BaseProductAttributeDto>? Attributes { get; set; } = new List<BaseProductAttributeDto>();

        public BaseSearchFilterMetadata? Filter { get; set; }

        public static List<SearchResponseDto> FromEntityList(ProductSearchRawResultDto products, IEnumerable<BaseCatalogAttributeDto> catalogAttributeGroups, bool isSummary = false)
        {
            return products.Results.Select(p => FromEntity(p, products.Filters, catalogAttributeGroups, isSummary)).ToList();
        }

        public static SearchResponseDto FromEntity(
        SearchItemDto entity,
        BaseSearchFilterMetadata Filter,
        IEnumerable<BaseCatalogAttributeDto> catalogAttributes,
        bool isSummary = false)
        {
            // Build dictionary from attribute definitions keyed by attribute key
            var attributeDict = catalogAttributes
                .ToDictionary(attr => attr.Key, attr => attr);

            var attributeValues = entity.AttributeValues ?? new List<BaseAttributeValue>();

            // If not summary view, only include values with matching definitions
            if (!isSummary)
            {
                attributeValues = attributeValues
                    .Where(attrVal => attrVal.AttributeKey != null && attributeDict.ContainsKey(attrVal.AttributeKey))
                    .ToList();
            }

            // Group values by attribute key
            var groupedAttributeValues = attributeValues
                .Where(val => !string.IsNullOrEmpty(val.AttributeKey))
                .GroupBy(val => val.AttributeKey!)
                .ToList();

            // Build ProductAttributeDto list
            var attributes = groupedAttributeValues.Select(group =>
            {
                var firstVal = group.First();
                attributeDict.TryGetValue(group.Key, out var definition);

                return new BaseProductAttributeDto
                {
                    Key = firstVal.AttributeKey!,
                    Label = firstVal.AttributeLabel ?? firstVal.AttributeKey!,
                    Values = group.Select(v => v.Value ?? "").Where(v => !string.IsNullOrWhiteSpace(v)).ToList(),
                    DataType = definition?.DataType ?? "String",
                    Icon = definition?.Icon
                };
            }).ToList();

            return new SearchResponseDto
            {
                Pid = entity.Pid.ToString(),
                //Cid = entity.Cid.ToString(),
                //Scid = entity.Scid.ToString(),
                Name = entity.Name,
                ThumbnailUrl = entity.ThumbnailUrl,
                //Cost = (double)entity.Cost,
                Rating = entity.Rating,
                Reviews = entity.Reviews,
                //CategoryType = "",
                //Quantity = 1,
                //Limit = 1,
                Filter = Filter,
                Attributes = attributes
            };
        }
    }
}
