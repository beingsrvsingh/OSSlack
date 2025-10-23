using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Commands;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Commands
{
    public class SeedProductCommandHandler : IRequestHandler<SeedCatalogCommand, Result>
    {
        private readonly ILoggerService<SeedProductCommandHandler> logger;
        private readonly ISeedProductService seedCatalogService;

        public SeedProductCommandHandler(ILoggerService<SeedProductCommandHandler> logger, ISeedProductService seedCatalogService)
        {
            this.logger = logger;
            this.seedCatalogService = seedCatalogService;
        }
        public async Task<Result> Handle(SeedCatalogCommand request, CancellationToken cancellationToken)
        {
            var products = new List<ProductMaster>
            {
                // SubCategory 101 - Kumkum (Vermilion)
                new ProductMaster
                {
                    Id = 1,  // Updated product id here
                    Name = "iPhone 15 Pro",
                    CategoryId = 1,
                    SubCategoryId = 101,
                    Rating = 4,
                    Reviews = 9,
                    SubCategoryNameSnapshot = "Smartphones",
                    CategoryNameSnapshot = "Electronics",
                    CreatedAt = DateTime.UtcNow,                    
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Basic Info Group (CatalogAttributeGroupId = 1)
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 1, // example CatalogAttribute ID for brand
                            CatalogAttributeGroupId = 1,
                            CatalogAttributeValueId = 1,
                            AttributeKey = "brand",
                            AttributeLabel = "Brand",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "Apple",
                            CreatedAt = DateTime.UtcNow
                        },

                        // Variant Info Group (CatalogAttributeGroupId = 3)
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 2, // example CatalogAttribute ID for color
                            CatalogAttributeGroupId = 3,
                            CatalogAttributeValueId = 4,
                            AttributeKey = "color",
                            AttributeLabel = "Color",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "Black",
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 2,
                            CatalogAttributeGroupId = 3,
                            CatalogAttributeValueId = 8,
                            AttributeKey = "color",
                            AttributeLabel = "Color",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "Silver",
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 2,
                            CatalogAttributeGroupId = 3,
                            CatalogAttributeValueId = 7,
                            AttributeKey = "color",
                            AttributeLabel = "Color",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "Gold",
                            CreatedAt = DateTime.UtcNow
                        },

                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 3, // example CatalogAttribute ID for storage
                            CatalogAttributeGroupId = 3,
                            CatalogAttributeValueId = 11,
                            AttributeKey = "storage",
                            AttributeLabel = "Storage Capacity",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "256GB",
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 3,
                            CatalogAttributeGroupId = 3,
                            CatalogAttributeValueId = 12,
                            AttributeKey = "storage",
                            AttributeLabel = "Storage Capacity",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "512GB",
                            CreatedAt = DateTime.UtcNow
                        },

                        // Technical Specs Group (CatalogAttributeGroupId = 2)
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 4, // example CatalogAttribute ID for ram
                            CatalogAttributeGroupId = 2,
                            CatalogAttributeValueId = 15,
                            AttributeKey = "ram",
                            AttributeLabel = "RAM",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "8GB",
                            CreatedAt = DateTime.UtcNow
                        },
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 5, // example CatalogAttribute ID for screen_size
                            CatalogAttributeGroupId = 2,
                            CatalogAttributeValueId = 17,
                            AttributeKey = "screen_size",
                            AttributeLabel = "Screen Size",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "6.1 inch",
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                },

                new ProductMaster { Id = 2, Name = "Natural Red Kumkum", CategoryId = 1, SubCategoryId = 101, SubCategoryNameSnapshot = "Kumkum (Vermilion)", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 123 }, 
                new ProductMaster { Id = 3, Name = "Organic Haldi Powder", CategoryId = 1, SubCategoryId = 102, SubCategoryNameSnapshot = "Haldi (Turmeric)", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 78 }, 
                new ProductMaster { Id = 4, Name = "Puja Haldi Stick", CategoryId = 1, SubCategoryId = 102, SubCategoryNameSnapshot = "Haldi (Turmeric)", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 64 }, 
                new ProductMaster { Id = 5, Name = "Chandan Powder", CategoryId = 1, SubCategoryId = 103, SubCategoryNameSnapshot = "Chandan (Sandalwood)", CategoryNameSnapshot = "Puja Samagri", Rating = 5, Reviews = 201 }, 
                new ProductMaster { Id = 6, Name = "Pure Sandalwood Stick", CategoryId = 1, SubCategoryId = 103, SubCategoryNameSnapshot = "Chandan (Sandalwood)", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 88 }, 
                new ProductMaster { Id = 7, Name = "Sacred Vibhuti", CategoryId = 1, SubCategoryId = 104, SubCategoryNameSnapshot = "Vibhuti / Bhasma", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 57 }, 
                new ProductMaster { Id = 8, Name = "Holy Bhasma Packet", CategoryId = 1, SubCategoryId = 104, SubCategoryNameSnapshot = "Vibhuti / Bhasma", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 45 }, 
                new ProductMaster { Id = 9, Name = "Traditional Sindoor", CategoryId = 1, SubCategoryId = 105, SubCategoryNameSnapshot = "Sindoor / Sacred Ash", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 90 }, 
                new ProductMaster { Id = 10, Name = "Sacred Ash Blend", CategoryId = 1, SubCategoryId = 105, SubCategoryNameSnapshot = "Sindoor / Sacred Ash", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 72 }, 
                new ProductMaster { Id = 11, Name = "Round Camphor Tablets", CategoryId = 1, SubCategoryId = 106, SubCategoryNameSnapshot = "Camphor (Kapoor)", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 55 }, 
                new ProductMaster { Id = 12, Name = "Organic Kapoor", CategoryId = 1, SubCategoryId = 106, SubCategoryNameSnapshot = "Camphor (Kapoor)", CategoryNameSnapshot = "Puja Samagri", Rating = 5, Reviews = 121 }, 
                new ProductMaster { Id = 13, Name = "Sandal Agarbatti", CategoryId = 1, SubCategoryId = 107, SubCategoryNameSnapshot = "Incense Sticks (Agarbatti)", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 80 }, 
                new ProductMaster { Id = 14, Name = "Floral Agarbatti", CategoryId = 1, SubCategoryId = 107, SubCategoryNameSnapshot = "Incense Sticks (Agarbatti)", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 67 }, 
                new ProductMaster { Id = 15, Name = "Dhoop Cones", CategoryId = 1, SubCategoryId = 108, SubCategoryNameSnapshot = "Dhoop Sticks / Cones", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 92 }, 
                new ProductMaster { Id = 16, Name = "Loban Dhoop Sticks", CategoryId = 1, SubCategoryId = 108, SubCategoryNameSnapshot = "Dhoop Sticks / Cones", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 110 }, 
                new ProductMaster { Id = 17, Name = "Puja Ghee Diya Oil", CategoryId = 1, SubCategoryId = 109, SubCategoryNameSnapshot = "Ghee / Oil for Lamps", CategoryNameSnapshot = "Puja Samagri", Rating = 5, Reviews = 145 }, 
                new ProductMaster { Id = 18, Name = "Pure Cow Ghee", CategoryId = 1, SubCategoryId = 109, SubCategoryNameSnapshot = "Ghee / Oil for Lamps", CategoryNameSnapshot = "Puja Samagri", Rating = 5, Reviews = 165 }, 
                new ProductMaster { Id = 19, Name = "Panchamrit Set", CategoryId = 1, SubCategoryId = 110, SubCategoryNameSnapshot = "Panchamrit Items", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 100 }, 
                new ProductMaster { Id = 20, Name = "Panchaamrut Combo", CategoryId = 1, SubCategoryId = 110, SubCategoryNameSnapshot = "Panchamrit Items", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 112 }, 
                new ProductMaster { Id = 21, Name = "Fresh Betel Leaves", CategoryId = 1, SubCategoryId = 111, SubCategoryNameSnapshot = "Betel Leaves & Nuts", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 53 }, 
                new ProductMaster { Id = 22, Name = "Supari (Areca Nut)", CategoryId = 1, SubCategoryId = 111, SubCategoryNameSnapshot = "Betel Leaves & Nuts", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 44 }, 
                new ProductMaster { Id = 23, Name = "Puja Almond Pack", CategoryId = 1, SubCategoryId = 112, SubCategoryNameSnapshot = "Dry Fruits", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 90 }, 
                new ProductMaster { Id = 24, Name = "Cashew for Puja", CategoryId = 1, SubCategoryId = 112, SubCategoryNameSnapshot = "Dry Fruits", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 85 }, 
                new ProductMaster { Id = 25, Name = "Marigold Garland", CategoryId = 1, SubCategoryId = 113, SubCategoryNameSnapshot = "Flowers & Garlands", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 130 }, 
                new ProductMaster { Id = 26, Name = "Fresh Rose Flowers", CategoryId = 1, SubCategoryId = 113, SubCategoryNameSnapshot = "Flowers & Garlands", CategoryNameSnapshot = "Puja Samagri", Rating = 3, Reviews = 95 }, 
                new ProductMaster { Id = 27, Name = "Puja Banana Pack", CategoryId = 1, SubCategoryId = 114, SubCategoryNameSnapshot = "Fruits for Puja", CategoryNameSnapshot = "Puja Samagri", Rating = 4, Reviews = 110 }, 
                new ProductMaster { Id = 28, Name = "Sacred Coconut", CategoryId = 1, SubCategoryId = 114, SubCategoryNameSnapshot = "Fruits for Puja", CategoryNameSnapshot = "Puja Samagri", Rating = 5, Reviews = 140 }, 
                new ProductMaster { Id = 29, Name = "Puja Thali Set", CategoryId = 2, SubCategoryId = 201, SubCategoryNameSnapshot = "Thali Sets", CategoryNameSnapshot = "Puja Essentials", Rating = 5, Reviews = 180 }, 
                new ProductMaster { Id = 30, Name = "Brass Puja Thali", CategoryId = 2, SubCategoryId = 201, SubCategoryNameSnapshot = "Thali Sets", CategoryNameSnapshot = "Puja Essentials", Rating = 4, Reviews = 120 }, 
                new ProductMaster { Id = 31, Name = "Copper Aarti Plate", CategoryId = 2, SubCategoryId = 202, SubCategoryNameSnapshot = "Aarti Plates", CategoryNameSnapshot = "Puja Essentials", Rating = 4, Reviews = 110 }, 
                new ProductMaster { Id = 32, Name = "Silver Plated Aarti", CategoryId = 2, SubCategoryId = 202, SubCategoryNameSnapshot = "Aarti Plates", CategoryNameSnapshot = "Puja Essentials", Rating = 5, Reviews = 90 }, 
                new ProductMaster { Id = 33, Name = "Brass Diya Set", CategoryId = 2, SubCategoryId = 203, SubCategoryNameSnapshot = "Diya Sets", CategoryNameSnapshot = "Puja Essentials", Rating = 4, Reviews = 130 }, 
                new ProductMaster { Id = 34, Name = "Clay Diya Pack", CategoryId = 2, SubCategoryId = 203, SubCategoryNameSnapshot = "Diya Sets", CategoryNameSnapshot = "Puja Essentials", Rating = 3, Reviews = 80 }, 
                new ProductMaster { Id = 35, Name = "Bell for Puja", CategoryId = 2, SubCategoryId = 204, SubCategoryNameSnapshot = "Puja Bells", CategoryNameSnapshot = "Puja Essentials", Rating = 4, Reviews = 75 }, 
                new ProductMaster { Id = 36, Name = "Copper Puja Bell", CategoryId = 2, SubCategoryId = 204, SubCategoryNameSnapshot = "Puja Bells", CategoryNameSnapshot = "Puja Essentials", Rating = 5, Reviews = 65 }, 
                new ProductMaster { Id = 37, Name = "Holy Kalash", CategoryId = 2, SubCategoryId = 205, SubCategoryNameSnapshot = "Kalash & Pots", CategoryNameSnapshot = "Puja Essentials", Rating = 5, Reviews = 140 }, 
                new ProductMaster { Id = 38, Name = "Brass Kalash", CategoryId = 2, SubCategoryId = 205, SubCategoryNameSnapshot = "Kalash & Pots", CategoryNameSnapshot = "Puja Essentials", Rating = 4, Reviews = 115 }, 
                new ProductMaster { Id = 39, Name = "Puja Thread (Mouli)", CategoryId = 2, SubCategoryId = 206, SubCategoryNameSnapshot = "Puja Threads", CategoryNameSnapshot = "Puja Essentials", Rating = 3, Reviews = 55 }, 
                new ProductMaster { Id = 40, Name = "Sacred Red Thread", CategoryId = 2, SubCategoryId = 206, SubCategoryNameSnapshot = "Puja Threads", CategoryNameSnapshot = "Puja Essentials", Rating = 4, Reviews = 50 }
            };

            // Add creation dates
            foreach (var product in products)
            {
                product.CreatedAt = DateTime.UtcNow;
            }

            SeedProductDto seedCatalogDto = new SeedProductDto
            {
                ProductMasters = products
            };

            bool isSeedCompleted = await seedCatalogService.SeedCatalogAsync(seedCatalogDto);

            if (!isSeedCompleted)
            {
                return Result.Failure(new FailureResponse("Seed Failed", "Catalog seed operation did not complete successfully."));
            }

            return Result.Success("Seeding completed successfully.");

        }
    }
}
