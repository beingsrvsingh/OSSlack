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
                    Price = 1299.99m,
                    SubCategoryId = 101,
                    SubCategoryNameSnapshot = "Smartphones",
                    CategoryNameSnapshot = "Electronics",
                    IsNew = true,
                    CreatedAt = DateTime.UtcNow,
                    AttributeValues = new List<ProductAttributeValue>
                    {
                        // Basic Info Group (CatalogAttributeGroupId = 1)
                        new()
                        {
                            ProductMasterId = 1,
                            CatalogAttributeId = 1, // example CatalogAttribute ID for brand
                            CatalogAttributeGroupId = 1,
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
                            AttributeKey = "screen_size",
                            AttributeLabel = "Screen Size",
                            AttributeDataTypeId = (int)AttributeDataType.String,
                            Value = "6.1 inch",
                            CreatedAt = DateTime.UtcNow
                        }
                    }
                },

                new() { Name = "Natural Red Kumkum", Price = 15.50m, SubCategoryId = 101, SubCategoryNameSnapshot = "Kumkum (Vermilion)", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 102 - Haldi (Turmeric)
                new() { Name = "Organic Haldi Powder", Price = 18.00m, SubCategoryId = 102, SubCategoryNameSnapshot = "Haldi (Turmeric)", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Puja Haldi Stick", Price = 12.75m, SubCategoryId = 102, SubCategoryNameSnapshot = "Haldi (Turmeric)", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 103 - Chandan (Sandalwood)
                new() { Name = "Chandan Powder", Price = 29.99m, SubCategoryId = 103, SubCategoryNameSnapshot = "Chandan (Sandalwood)", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new() { Name = "Pure Sandalwood Stick", Price = 45.00m, SubCategoryId = 103, SubCategoryNameSnapshot = "Chandan (Sandalwood)", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 104 - Vibhuti / Bhasma
                new() { Name = "Sacred Vibhuti", Price = 9.99m, SubCategoryId = 104, SubCategoryNameSnapshot = "Vibhuti / Bhasma", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Holy Bhasma Packet", Price = 8.49m, SubCategoryId = 104, SubCategoryNameSnapshot = "Vibhuti / Bhasma", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 105 - Sindoor / Sacred Ash
                new() { Name = "Traditional Sindoor", Price = 14.20m, SubCategoryId = 105, SubCategoryNameSnapshot = "Sindoor / Sacred Ash", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Sacred Ash Blend", Price = 11.75m, SubCategoryId = 105, SubCategoryNameSnapshot = "Sindoor / Sacred Ash", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 106 - Camphor (Kapoor)
                new() { Name = "Round Camphor Tablets", Price = 6.90m, SubCategoryId = 106, SubCategoryNameSnapshot = "Camphor (Kapoor)", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Organic Kapoor", Price = 7.50m, SubCategoryId = 106, SubCategoryNameSnapshot = "Camphor (Kapoor)", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },

                // SubCategory 107 - Incense Sticks (Agarbatti)
                new() { Name = "Sandal Agarbatti", Price = 12.00m, SubCategoryId = 107, SubCategoryNameSnapshot = "Incense Sticks (Agarbatti)", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Floral Agarbatti", Price = 10.50m, SubCategoryId = 107, SubCategoryNameSnapshot = "Incense Sticks (Agarbatti)", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 108 - Dhoop Sticks / Cones
                new() { Name = "Dhoop Cones", Price = 13.49m, SubCategoryId = 108, SubCategoryNameSnapshot = "Dhoop Sticks / Cones", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Loban Dhoop Sticks", Price = 14.20m, SubCategoryId = 108, SubCategoryNameSnapshot = "Dhoop Sticks / Cones", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 109 - Ghee / Oil for Lamps
                new() { Name = "Puja Ghee Diya Oil", Price = 22.99m, SubCategoryId = 109, SubCategoryNameSnapshot = "Ghee / Oil for Lamps", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Pure Cow Ghee", Price = 28.00m, SubCategoryId = 109, SubCategoryNameSnapshot = "Ghee / Oil for Lamps", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 110 - Panchamrit Items
                new() { Name = "Panchamrit Set", Price = 18.90m, SubCategoryId = 110, SubCategoryNameSnapshot = "Panchamrit Items", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Panchaamrut Combo", Price = 20.99m, SubCategoryId = 110, SubCategoryNameSnapshot = "Panchamrit Items", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 111 - Betel Leaves & Nuts
                new() { Name = "Fresh Betel Leaves", Price = 9.20m, SubCategoryId = 111, SubCategoryNameSnapshot = "Betel Leaves & Nuts", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Supari (Areca Nut)", Price = 11.60m, SubCategoryId = 111, SubCategoryNameSnapshot = "Betel Leaves & Nuts", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 112 - Dry Fruits
                new() { Name = "Puja Almond Pack", Price = 25.50m, SubCategoryId = 112, SubCategoryNameSnapshot = "Dry Fruits", CategoryNameSnapshot = "Puja Samagri", IsNew = true },
                new() { Name = "Cashew for Puja", Price = 24.90m, SubCategoryId = 112, SubCategoryNameSnapshot = "Dry Fruits", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 113 - Flowers & Garlands
                new() { Name = "Marigold Garland", Price = 15.00m, SubCategoryId = 113, SubCategoryNameSnapshot = "Flowers & Garlands", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Fresh Rose Flowers", Price = 13.75m, SubCategoryId = 113, SubCategoryNameSnapshot = "Flowers & Garlands", CategoryNameSnapshot = "Puja Samagri" },

                // SubCategory 114 - Fruits for Offering
                new() { Name = "Puja Banana Pack", Price = 12.40m, SubCategoryId = 114, SubCategoryNameSnapshot = "Fruits for Offering", CategoryNameSnapshot = "Puja Samagri" },
                new() { Name = "Seasonal Fruits Basket", Price = 19.99m, SubCategoryId = 114, SubCategoryNameSnapshot = "Fruits for Offering", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },

                new ProductMaster { Name = "Brass Kalash", Price = 49.99m, SubCategoryId = 201, SubCategoryNameSnapshot = "Kalash", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "Silver Kalash", Price = 89.99m, SubCategoryId = 201, SubCategoryNameSnapshot = "Kalash", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Pooja Thali Set", Price = 29.99m, SubCategoryId = 202, SubCategoryNameSnapshot = "Pooja Thali", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Decorative Pooja Thali", Price = 35.50m, SubCategoryId = 202, SubCategoryNameSnapshot = "Pooja Thali", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "Seasonal Fruits Basket", Price = 19.99m, SubCategoryId = 114, SubCategoryNameSnapshot = "Fruits for Offering", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },

                new ProductMaster { Name = "Cotton Altar Cloth", Price = 15.99m, SubCategoryId = 301, SubCategoryNameSnapshot = "Altar / Mandir Cloths", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "Silk Altar Cloth", Price = 35.99m, SubCategoryId = 301, SubCategoryNameSnapshot = "Altar / Mandir Cloths", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Traditional Deity Dress", Price = 55.00m, SubCategoryId = 302, SubCategoryNameSnapshot = "Deity Dresses (Vastra)", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Festive Deity Dress", Price = 75.50m, SubCategoryId = 302, SubCategoryNameSnapshot = "Deity Dresses (Vastra)", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },

                new ProductMaster { Name = "Brass Metal Idol", Price = 120.00m, SubCategoryId = 401, SubCategoryNameSnapshot = "Metal Idols", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Copper Metal Idol", Price = 95.50m, SubCategoryId = 401, SubCategoryNameSnapshot = "Metal Idols", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "White Marble Statue", Price = 250.00m, SubCategoryId = 402, SubCategoryNameSnapshot = "Marble Statues", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Polished Marble Statue", Price = 320.75m, SubCategoryId = 402, SubCategoryNameSnapshot = "Marble Statues", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },

                new ProductMaster { Name = "Rudraksha Mala 108 Beads", Price = 350.00m, SubCategoryId = 501, SubCategoryNameSnapshot = "Rudraksha Mala", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Rudraksha Mala 54 Beads", Price = 180.00m, SubCategoryId = 501, SubCategoryNameSnapshot = "Rudraksha Mala", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "Tulsi Mala Small", Price = 75.00m, SubCategoryId = 502, SubCategoryNameSnapshot = "Tulsi Mala", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "Tulsi Mala Large", Price = 150.00m, SubCategoryId = 502, SubCategoryNameSnapshot = "Tulsi Mala", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },

                new ProductMaster { Name = "Diwali Festival Kit", Price = 499.99m, SubCategoryId = 601, SubCategoryNameSnapshot = "Festival Kits", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Navratri Festival Kit", Price = 399.99m, SubCategoryId = 601, SubCategoryNameSnapshot = "Festival Kits", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },
                new ProductMaster { Name = "Basic Graha Pravesh Kit", Price = 599.99m, SubCategoryId = 602, SubCategoryNameSnapshot = "Graha Pravesh Kit", CategoryNameSnapshot = "Puja Samagri", IsFeatured = true },
                new ProductMaster { Name = "Advanced Graha Pravesh Kit", Price = 999.99m, SubCategoryId = 602, SubCategoryNameSnapshot = "Graha Pravesh Kit", CategoryNameSnapshot = "Puja Samagri", IsFeatured = false },

                new ProductMaster { Name = "Bhagavad Gita Hardcover", Price = 299.99m, SubCategoryId = 701, SubCategoryNameSnapshot = "Bhagavad Gita", CategoryNameSnapshot = "Spiritual Books", IsFeatured = true },
                new ProductMaster { Name = "Ramayana Illustrated Edition", Price = 349.99m, SubCategoryId = 702, SubCategoryNameSnapshot = "Ramayana", CategoryNameSnapshot = "Spiritual Books", IsFeatured = false },

                new ProductMaster { Name = "Natural Attar Perfume Set", Price = 199.99m, SubCategoryId = 801, SubCategoryNameSnapshot = "Attar / Natural Perfume", CategoryNameSnapshot = "Fragrances", IsFeatured = true },
                new ProductMaster { Name = "Premium Sambrani Pack", Price = 149.99m, SubCategoryId = 802, SubCategoryNameSnapshot = "Sambrani", CategoryNameSnapshot = "Fragrances", IsFeatured = false },

                new ProductMaster { Name = "Traditional Wooden Mandir", Price = 4999.99m, SubCategoryId = 901, SubCategoryNameSnapshot = "Mandirs (Temples)", CategoryNameSnapshot = "Temple Accessories", IsFeatured = true },
                new ProductMaster { Name = "Brass Temple Bell", Price = 799.99m, SubCategoryId = 902, SubCategoryNameSnapshot = "Temple Bells", CategoryNameSnapshot = "Temple Accessories", IsFeatured = false },

                new ProductMaster { Name = "Donation Box Wooden", Price = 999.99m, SubCategoryId = 1001, SubCategoryNameSnapshot = "Donation Boxes", CategoryNameSnapshot = "Donation Items", IsFeatured = true },
                new ProductMaster { Name = "Copper Coin Set", Price = 499.99m, SubCategoryId = 1002, SubCategoryNameSnapshot = "Copper / Silver Coins", CategoryNameSnapshot = "Donation Items", IsFeatured = false },

                new ProductMaster { Name = "Archana Name Recital Service", Price = 249.99m, SubCategoryId = 1101, SubCategoryNameSnapshot = "Archana / Name Recital", CategoryNameSnapshot = "Pooja Services", IsFeatured = true },
                new ProductMaster { Name = "Abhishekam Ritual Service", Price = 399.99m, SubCategoryId = 1102, SubCategoryNameSnapshot = "Abhishekam", CategoryNameSnapshot = "Pooja Services", IsFeatured = false },

                new ProductMaster { Name = "Vedic Astrology Consultation", Price = 1999.99m, SubCategoryId = 1200, SubCategoryNameSnapshot = "Vedic Astrology", CategoryNameSnapshot = "Astrology", IsFeatured = true },
                new ProductMaster { Name = "Western Astrology Report", Price = 1499.99m, SubCategoryId = 1201, SubCategoryNameSnapshot = "Western Astrology", CategoryNameSnapshot = "Astrology", IsFeatured = false },

                new ProductMaster { Name = "Numerology Detailed Reading", Price = 1299.99m, SubCategoryId = 1301, SubCategoryNameSnapshot = "Numerology Reading", CategoryNameSnapshot = "Numerology", IsFeatured = true },
                new ProductMaster { Name = "Lucky Number Prediction Report", Price = 999.99m, SubCategoryId = 1302, SubCategoryNameSnapshot = "Lucky Number Prediction", CategoryNameSnapshot = "Numerology", IsFeatured = false },

                new ProductMaster { Name = "General Tarot Reading Session", Price = 1499.99m, SubCategoryId = 1401, SubCategoryNameSnapshot = "General Tarot Reading", CategoryNameSnapshot = "Tarot", IsFeatured = true },
                new ProductMaster { Name = "Love & Relationship Tarot Reading", Price = 1699.99m, SubCategoryId = 1402, SubCategoryNameSnapshot = "Love & Relationship Tarot", CategoryNameSnapshot = "Tarot", IsFeatured = false },

                new ProductMaster { Name = "Life Line Palm Reading", Price = 899.99m, SubCategoryId = 1502, SubCategoryNameSnapshot = "Life Line Reading", CategoryNameSnapshot = "Palmistry", IsFeatured = true },
                new ProductMaster { Name = "Heart Line Palm Reading", Price = 899.99m, SubCategoryId = 1503, SubCategoryNameSnapshot = "Heart Line Reading", CategoryNameSnapshot = "Palmistry", IsFeatured = false },

                new ProductMaster { Name = "Psychic Reading Session", Price = 2499.99m, SubCategoryId = 1601, SubCategoryNameSnapshot = "Psychic Reading", CategoryNameSnapshot = "Spiritual Guidance", IsFeatured = true },
                new ProductMaster { Name = "Intuitive Guidance Counseling", Price = 1999.99m, SubCategoryId = 1602, SubCategoryNameSnapshot = "Intuitive Guidance", CategoryNameSnapshot = "Spiritual Guidance", IsFeatured = false },

                new ProductMaster { Name = "Residential Vastu Consultation", Price = 2999.99m, SubCategoryId = 1801, SubCategoryNameSnapshot = "Residential Vastu Consultation", CategoryNameSnapshot = "Vastu", IsFeatured = true },
                new ProductMaster { Name = "Commercial Vastu Services", Price = 3999.99m, SubCategoryId = 1802, SubCategoryNameSnapshot = "Commercial Vastu (Shops, Offices)", CategoryNameSnapshot = "Vastu", IsFeatured = false },

                new ProductMaster { Name = "Personal Astrology Consultation", Price = 2499.99m, SubCategoryId = 1901, SubCategoryNameSnapshot = "Personal Astrology Consultation", CategoryNameSnapshot = "Astrology", IsFeatured = true },
                new ProductMaster { Name = "Numerology Consultation Service", Price = 1999.99m, SubCategoryId = 1902, SubCategoryNameSnapshot = "Numerology Consultation", CategoryNameSnapshot = "Numerology", IsFeatured = false },

                new ProductMaster { Name = "Vedic Puja Services", Price = 1599.99m, SubCategoryId = 2001, SubCategoryNameSnapshot = "Vedic Puja Services (Ganesh, Lakshmi, etc.)", CategoryNameSnapshot = "Puja", IsFeatured = true },
                new ProductMaster { Name = "Havan / Homa Rituals", Price = 1799.99m, SubCategoryId = 2002, SubCategoryNameSnapshot = "Havan / Homa (Navagraha, Rudra, etc.)", CategoryNameSnapshot = "Puja", IsFeatured = false },

                new ProductMaster { Name = "Kundli Generation Service", Price = 999.99m, SubCategoryId = 2101, SubCategoryNameSnapshot = "Kundli Generation (Online/Offline)", CategoryNameSnapshot = "Kundli", IsFeatured = true },
                new ProductMaster { Name = "Janam Kundli (Hindi/English)", Price = 1099.99m, SubCategoryId = 2102, SubCategoryNameSnapshot = "Janam Kundli in Hindi/English", CategoryNameSnapshot = "Kundli", IsFeatured = false },

                new ProductMaster { Name = "North Indian Pandit Booking", Price = 2999.99m, SubCategoryId = 2201, SubCategoryNameSnapshot = "North Indian Pandit", CategoryNameSnapshot = "Pandit Services", IsFeatured = true },
                new ProductMaster { Name = "South Indian Pandit Booking", Price = 2999.99m, SubCategoryId = 2202, SubCategoryNameSnapshot = "South Indian Pandit", CategoryNameSnapshot = "Pandit Services", IsFeatured = false },

                new ProductMaster { Name = "Temple Darshan Booking", Price = 499.99m, SubCategoryId = 2301, SubCategoryNameSnapshot = "Temple Darshan Booking", CategoryNameSnapshot = "Temple Services", IsFeatured = true },
                new ProductMaster { Name = "Online Temple Puja Service", Price = 999.99m, SubCategoryId = 2302, SubCategoryNameSnapshot = "Online Temple Puja", CategoryNameSnapshot = "Temple Services", IsFeatured = false },

                new ProductMaster { Name = "Bhajan Mandali Booking", Price = 2999.99m, SubCategoryId = 2401, SubCategoryNameSnapshot = "Bhajan Mandali Booking", CategoryNameSnapshot = "Event Services", IsFeatured = true },
                new ProductMaster { Name = "Jagran Event Booking", Price = 3999.99m, SubCategoryId = 2402, SubCategoryNameSnapshot = "Jagran Event", CategoryNameSnapshot = "Event Services", IsFeatured = false },


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
