using Catalog.Application.Contracts;
using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
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
            // Seed Categories
            var categories = new List<CategoryMaster>
        {
            new CategoryMaster { Id = 1, Name = "Temple Pooja", Description = "Temple related pooja and services", DisplayOrder = 1, IsActive = true },
            new CategoryMaster { Id = 2, Name = "Home Pooja", Description = "Home based pooja and rituals", DisplayOrder = 2, IsActive = true },
            new CategoryMaster { Id = 3, Name = "Astrologer", Description = "Astrology related services", DisplayOrder = 3, IsActive = true },
            new CategoryMaster { Id = 4, Name = "Pooja Essentials", Description = "Essentials for pooja rituals", DisplayOrder = 4, IsActive = true },
            new CategoryMaster { Id = 5, Name = "Samagri Kits", Description = "Pre-packed pooja kits", DisplayOrder = 5, IsActive = true }
            // add more categories as needed
        };


            // Seed SubCategories
            var subcategories = new List<SubCategoryMaster>
        {
            // Temple Pooja (CategoryId=1)
            new SubCategoryMaster { Id=100, CategoryMasterId=1, Name="Aarti", SubcategoryType=SubcategoryType.Service, IsActive=true },
            new SubCategoryMaster { Id=110, CategoryMasterId=1, Name="Mangala Aarti", ParentSubcategoryId=100, SubcategoryType=SubcategoryType.Aarti, IsActive=true },
            new SubCategoryMaster { Id=111, CategoryMasterId=1, Name="Sandhya Aarti", ParentSubcategoryId=100, SubcategoryType=SubcategoryType.Aarti, IsActive=true },

            new SubCategoryMaster { Id=101, CategoryMasterId=1, Name="Donation", SubcategoryType=SubcategoryType.Service, IsActive=true },
            new SubCategoryMaster { Id=120, CategoryMasterId=1, Name="General Donation", ParentSubcategoryId=101, SubcategoryType=SubcategoryType.Donation, IsActive=true },
            new SubCategoryMaster { Id=121, CategoryMasterId=1, Name="Annadanam", ParentSubcategoryId=101, SubcategoryType=SubcategoryType.Donation, IsActive=true },

            new SubCategoryMaster { Id=102, CategoryMasterId=1, Name="Prasad", SubcategoryType=SubcategoryType.Service, IsActive=true },
            new SubCategoryMaster { Id=130, CategoryMasterId=1, Name="Prasad Box", ParentSubcategoryId=102, SubcategoryType=SubcategoryType.Prasad, IsActive=true },

            new SubCategoryMaster { Id=103, CategoryMasterId=1, Name="Pooja", SubcategoryType=SubcategoryType.Service, IsActive=true },
            new SubCategoryMaster { Id=140, CategoryMasterId=1, Name="Rudra Abhishek", ParentSubcategoryId=103, SubcategoryType=SubcategoryType.Pooja, IsActive=true },
            new SubCategoryMaster { Id=141, CategoryMasterId=1, Name="Ganesh Pooja", ParentSubcategoryId=103, SubcategoryType=SubcategoryType.Pooja, IsActive=true },

            new SubCategoryMaster { Id=104, CategoryMasterId=1, Name="Bhajan/Kirtan", SubcategoryType=SubcategoryType.Service, IsActive=true },
            new SubCategoryMaster { Id=150, CategoryMasterId=1, Name="Satsang Bhajan", ParentSubcategoryId=104, SubcategoryType=SubcategoryType.Bhajan, IsActive=true },
            new SubCategoryMaster { Id=151, CategoryMasterId=1, Name="Kirtan Night", ParentSubcategoryId=104, SubcategoryType=SubcategoryType.Bhajan, IsActive=true },

            new SubCategoryMaster { Id=105, CategoryMasterId=1, Name="Bhandara", SubcategoryType=SubcategoryType.Service, IsActive=true },
            new SubCategoryMaster { Id=160, CategoryMasterId=1, Name="Sponsor Bhandara", ParentSubcategoryId=105, SubcategoryType=SubcategoryType.Bhandara, IsActive=true },
            new SubCategoryMaster { Id=161, CategoryMasterId=1, Name="Attend Bhandara", ParentSubcategoryId=105, SubcategoryType=SubcategoryType.Bhandara, IsActive=true },

            // Samagri Kits (CategoryId=5)
            new SubCategoryMaster { Id=501, CategoryMasterId=5, Name="Ganesh Pooja Kit", SubcategoryType=SubcategoryType.Kit, IsActive=true },
            new SubCategoryMaster { Id=502, CategoryMasterId=5, Name="Lakshmi Pooja Kit", SubcategoryType=SubcategoryType.Kit, IsActive=true },

            // Product Subcategories (for Pooja Essentials, CategoryId=4)
            new SubCategoryMaster { Id=601, CategoryMasterId=4, Name="Incense Sticks", SubcategoryType=SubcategoryType.Product, IsActive=true },
            new SubCategoryMaster { Id=602, CategoryMasterId=4, Name="Diyas", SubcategoryType=SubcategoryType.Product, IsActive=true },
            new SubCategoryMaster { Id=603, CategoryMasterId=4, Name="Camphor", SubcategoryType=SubcategoryType.Product, IsActive=true },
            new SubCategoryMaster { Id=604, CategoryMasterId=4, Name="Decorative Items", SubcategoryType=SubcategoryType.Product, IsActive=true }
        };



            // Seed PoojaKitItems
            var poojaKitItems = new List<PoojaKitItemMaster>
        {
            new PoojaKitItemMaster { Id=1, KitSubcategoryId=501, ProductSubcategoryId=601},
            new PoojaKitItemMaster { Id=2, KitSubcategoryId=501, ProductSubcategoryId=602},
            new PoojaKitItemMaster { Id=3, KitSubcategoryId=501, ProductSubcategoryId=603},

            new PoojaKitItemMaster { Id=4, KitSubcategoryId=502, ProductSubcategoryId=601},
            new PoojaKitItemMaster { Id=5, KitSubcategoryId=502, ProductSubcategoryId=602},
            new PoojaKitItemMaster { Id=6, KitSubcategoryId=502, ProductSubcategoryId=603},
            new PoojaKitItemMaster { Id=7, KitSubcategoryId=502, ProductSubcategoryId=604, Notes = "Decorative item" }
        };

            SeedCatalogDto seedCatalogDto = new SeedCatalogDto
            {
                CategoryMasters = categories,
                SubCategoryMasters = subcategories,
                PoojaKitItems = poojaKitItems,
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