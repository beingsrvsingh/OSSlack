using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Shared.Application.Common.Contracts.Response;
using Shared.Domain.Enums;
using System.Linq.Expressions;

namespace Product.Infrastructure.Persistence.Catalog.Queries
{
    public class CatalogQueries
    {
        public static IQueryable<ProductMaster> ApplySearch(
        IQueryable<ProductMaster> query,
        string? search)
        {
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    EF.Functions.Like(p.Name, $"%{search}%"));
            }

            return query;
        }

        public static readonly Expression<Func<ProductMaster, CatalogResponseDto>> ToCatalogResponse =
            p => new CatalogResponseDto
            {
                Id = p.Id.ToString(),
                Name = p.Name,
                ThumbnailUrl = p.ThumbnailUrl,
                Rating = p.Rating,
                Reviews = p.Reviews,
                SubCategoryId = p.SubCategoryId.ToString(),
                IsTrending = p.IsTrending,
                IsFeatured = p.IsFeatured,

                Price = new PriceResponseDto
                {
                    Amount = p.Price.Amount,
                    Currency = p.Price.Currency,
                    Discount = p.Price.Discount,
                    Mrp = p.Price.Mrp,
                    Tax = p.Price.Tax
                },

                // Media
                Media = p.Media.Select(img => new MediaResponseDto
                {
                    Url = img.ImageUrl,
                    Type = img.MediaType.ToString(),
                    AltText = img.AltText,
                    SortOrder = img.SortOrder
                }).ToList(),

                // Addons
                Addons = p.Addons.Select(a => new AddonResponseDto
                {
                    Name = a.Name,
                    Description = a.Description,
                    Price = new PriceResponseDto
                    {
                        Amount = a.Price.Amount,
                        Mrp = a.Price.Mrp
                    }
                }).ToList(),

                // Attributes
                Attributes = p.AttributeValues.Select(a => new AttributeResponseDto
                {
                    Key = a.AttributeKey,
                    Label = a.AttributeLabel ?? "",
                    Value = a.Value,
                    DataTypeId = a.AttributeDataTypeId
                        ?? (int)AttributeDataType.String
                }).ToList(),

                // Variants
                Variants = p.VariantMasters.Select(v => new CatalogVariantResponseDto
                {
                    Id = v.Id.ToString(),
                    Name = v.Name,
                    StockQuantity = v.StockQuantity,

                    Price = new PriceResponseDto
                    {
                        Amount = v.Price.Amount,
                        Currency = v.Price.Currency,
                        Discount = v.Price.Discount,
                        Mrp = v.Price.Mrp,
                        Tax = v.Price.Tax
                    },

                    Attributes = v.Attributes
                        .GroupBy(a => a.AttributeGroupNameSnapshot)
                        .Select(g => new AttributeGroupResponseDto
                        {
                            AttributeGroupName = g.Key,
                            Attributes = g.Select(a => new AttributeResponseDto
                            {
                                Key = a.AttributeKey,
                                Label = a.AttributeLabel ?? "",
                                Value = a.Value,
                                DataTypeId = a.AttributeDataTypeId
                                    ?? (int)AttributeDataType.String
                            }).ToList()
                        }).ToList(),

                    Addons = v.Addons.Select(a => new AddonResponseDto
                    {
                        Name = a.Name,
                        Description = a.Description,
                        Price = new PriceResponseDto
                        {
                            Amount = a.Price.Amount,
                            Mrp = a.Price.Mrp
                        }
                    }).ToList(),

                    Media = v.Media.Select(img => new MediaResponseDto
                    {
                        Url = img.ImageUrl,
                        Type = img.MediaType.ToString(),
                        AltText = img.AltText,
                        SortOrder = img.SortOrder
                    }).ToList()
                }).ToList()
            };
    }
}
