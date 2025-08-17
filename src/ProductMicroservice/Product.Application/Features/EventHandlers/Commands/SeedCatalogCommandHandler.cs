using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Commands;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Commands
{
    public class SeedCatalogCommandHandler : IRequestHandler<SeedCatalogCommand, Result>
    {
        private readonly ILoggerService<SeedCatalogCommandHandler> logger;
        private readonly ISeedCatalogService seedCatalogService;

        public SeedCatalogCommandHandler(ILoggerService<SeedCatalogCommandHandler> logger, ISeedCatalogService seedCatalogService)
        {
            this.logger = logger;
            this.seedCatalogService = seedCatalogService;
        }
        public async Task<Result> Handle(SeedCatalogCommand request, CancellationToken cancellationToken)
        {
            var products = new List<ProductMaster>
            {
                // SubCategory 101 - Kumkum (Vermilion)
                new() { Name = "Premium Kumkum", Price = 25.99m, SubCategoryId = 101, SubCategoryNameSnapshot = "Kumkum (Vermilion)", CategoryNameSnapshot = "Puja Samagri", IsNew = true },
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
            };

            // Add creation dates
            foreach (var product in products)
            {
                product.CreatedAt = DateTime.UtcNow;
            }

            SeedCatalogDto seedCatalogDto = new SeedCatalogDto
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
