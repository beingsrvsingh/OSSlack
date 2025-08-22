using Catalog.Application.Contracts;
using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
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
            var categories = new List<CategoryMaster>
            {
                new CategoryMaster { Id = 1, CategoryType = "product", Name = "Pooja Samagri", Description = "Worship essentials used in rituals", DisplayOrder = 1, IsActive = true },
                new CategoryMaster { Id = 2, CategoryType = "product", Name = "Pooja Vessels & Utensils", Description = "Traditional utensils used during pooja", DisplayOrder = 2, IsActive = true },
                new CategoryMaster { Id = 3, CategoryType = "product", Name = "Pooja Cloths & Decoration", Description = "Clothing and decorative items for pooja", DisplayOrder = 3, IsActive = true },
                new CategoryMaster { Id = 4, CategoryType = "product", Name = "Deities & Idols", Description = "Idols and photos for worship", DisplayOrder = 4, IsActive = true },
                new CategoryMaster { Id = 5, CategoryType = "product", Name = "Spiritual & Ritual Items", Description = "Spiritual accessories and tools", DisplayOrder = 5, IsActive = true },
                new CategoryMaster { Id = 6, CategoryType = "product", Name = "Pooja Kits / Combo Packs", Description = "Ready-made kits for various poojas", DisplayOrder = 6, IsActive = true },
                new CategoryMaster { Id = 7, CategoryType = "product", Name = "Holy Scriptures & Books", Description = "Religious books and pooja guides", DisplayOrder = 7, IsActive = true },
                new CategoryMaster { Id = 8, CategoryType = "product", Name = "Aroma & Fragrance Items", Description = "Fragrance products for spiritual ambiance", DisplayOrder = 8, IsActive = true },
                new CategoryMaster { Id = 9, CategoryType = "product", Name = "Pooja Mandir Accessories", Description = "Items used in or for mandirs", DisplayOrder = 9, IsActive = true },
                new CategoryMaster { Id = 10, CategoryType = "product",  Name = "Miscellaneous", Description = "Other spiritual or pooja-related items", DisplayOrder = 10, IsActive = true },
                new CategoryMaster { Id = 11, CategoryType = "pooja",  Name = "Pooja", Description = "All types of poojas and rituals conducted in homes", DisplayOrder = 1, IsActive = true },
                new CategoryMaster { Id = 12, CategoryType = "astrologer",  Name = "Astrology", Description = "Get insights and predictions based on astrological systems", DisplayOrder = 2, IsActive = true },
                new CategoryMaster { Id = 13, CategoryType = "astrologer",  Name = "Numerology", Description = "Discover your life path and destiny numbers with numerology", DisplayOrder = 3, IsActive = true },
                new CategoryMaster { Id = 14, CategoryType = "astrologer",  Name = "Tarot", Description = "Receive intuitive guidance through Tarot card readings", DisplayOrder = 4, IsActive = true },
                new CategoryMaster { Id = 15, CategoryType = "astrologer",  Name = "Palmistry", Description = "Interpret lines and mounts on your palm for future insights", DisplayOrder = 5, IsActive = true },
                new CategoryMaster { Id = 16, CategoryType = "astrologer",  Name = "Psychic", Description = "Connect with psychics for intuitive and spiritual readings", DisplayOrder = 6, IsActive = true },
                new CategoryMaster { Id = 17, CategoryType = "astrologer",  Name = "Healing", Description = "Spiritual and energy-based healing for mind, body, and soul", DisplayOrder = 7, IsActive = true },
                new CategoryMaster { Id = 18, CategoryType = "astrologer",  Name = "Vastu", Description = "Vastu consultation for home, office, and buildings", DisplayOrder = 8, IsActive = true },
                new CategoryMaster { Id = 19, CategoryType = "astrologer",  Name = "Consultation", Description = "Book personalized sessions with experts across domains", DisplayOrder = 9, IsActive = true },
                new CategoryMaster { Id = 20, CategoryType = "priest",  Name = "Rituals", Description = "Perform religious rituals and ceremonies for various needs", DisplayOrder = 10, IsActive = true },
                new CategoryMaster { Id = 21, CategoryType = "astrologer",  Name = "Kundli", Description = "Get your Kundli created, analyzed, and matched", DisplayOrder = 11, IsActive = true },
                new CategoryMaster { Id = 22, CategoryType = "priest",  Name = "Priest", Description = "Get your Kundli created, analyzed, and matched", DisplayOrder = 11, IsActive = true },
                new CategoryMaster { Id = 23, CategoryType = "temple",  Name = "Temple", Description = "Get your Kundli created, analyzed, and matched", DisplayOrder = 11, IsActive = true },
                new CategoryMaster { Id = 24, CategoryType = "kathavachak",  Name = "Bhajan & Katha Seva", Description = "Get your Kundli created, analyzed, and matched", DisplayOrder = 11, IsActive = true }
            };

            var subcategories = new List<SubCategoryMaster>
            {
                // 1. Pooja Samagri
                new SubCategoryMaster { Id = 101, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Kumkum (Vermilion)", IsActive = true,
                CatalogAttributes = new List<CatalogAttribute>()
                {
                    new CatalogAttribute
                    {
                        Id = 201,
                        Key = "color",
                        Label = "Color",
                        DataType = AttributeDataType.Enum,
                        IsCustom = false,
                        IsRequired = true,
                        SortOrder = 1,
                        AllowedValues = new List<CatalogAttributeAllowedValue>
                        {
                            new CatalogAttributeAllowedValue { Value = "Red", SortOrder = 1, CreatedAt = DateTime.UtcNow },
                            new CatalogAttributeAllowedValue { Value = "Yellow", SortOrder = 2, CreatedAt = DateTime.UtcNow },
                            new CatalogAttributeAllowedValue { Value = "Orange", SortOrder = 3, CreatedAt = DateTime.UtcNow }
                        },
                        CatalogAttributeIcon = new CatalogAttributeIcon
                        {
                            IconName = "palette",
                            IconCodePoint = 0xe40a, // Material icon for "palette"
                            IconFontFamily = "MaterialIcons"
                        },
                        CreatedAt = DateTime.UtcNow
                    },
                    new CatalogAttribute
                    {
                        Id = 202,
                        Key = "weight",
                        Label = "Weight",
                        DataType = AttributeDataType.String,
                        IsCustom = false,
                        IsRequired = false,
                        SortOrder = 2,
                        CatalogAttributeIcon = new CatalogAttributeIcon
                        {
                            IconName = "scale", // Or "balance", depending on available icons
                            IconCodePoint = 0xf5de, // Example Material Design Icon (if using MDI fonts)
                            IconFontFamily = "MaterialIcons"
                        },
                        CreatedAt = DateTime.UtcNow
                    },
                    new CatalogAttribute
                    {
                        Id = 203,
                        Key = "brand",
                        Label = "Brand",
                        DataType = AttributeDataType.Enum,
                        IsCustom = false,
                        IsRequired = false,
                        SortOrder = 3,
                        AllowedValues = new List<CatalogAttributeAllowedValue>
                        {
                            new CatalogAttributeAllowedValue { Value = "UnPooja", SortOrder = 1, CreatedAt = DateTime.UtcNow },
                        },
                        CatalogAttributeIcon = new CatalogAttributeIcon
                        {
                            IconName = "category",
                            IconCodePoint = 0xe574,
                            IconFontFamily = "MaterialIcons"
                        },
                        CreatedAt = DateTime.UtcNow
                    }            
                } },
                new SubCategoryMaster { Id = 102, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Haldi (Turmeric)", IsActive = true },
                new SubCategoryMaster { Id = 103, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Chandan (Sandalwood)", IsActive = true },
                new SubCategoryMaster { Id = 104, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Vibhuti / Bhasma", IsActive = true },
                new SubCategoryMaster { Id = 105, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Sindoor / Sacred Ash", IsActive = true },
                new SubCategoryMaster { Id = 106, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Camphor (Kapoor)", IsActive = true },
                new SubCategoryMaster { Id = 107, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Incense Sticks (Agarbatti)", IsActive = true },
                new SubCategoryMaster { Id = 108, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Dhoop Sticks / Cones", IsActive = true },
                new SubCategoryMaster { Id = 109, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Ghee / Oil for Lamps", IsActive = true },
                new SubCategoryMaster { Id = 110, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Panchamrit Items", IsActive = true },
                new SubCategoryMaster { Id = 111, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Betel Leaves & Nuts", IsActive = true },
                new SubCategoryMaster { Id = 112, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Dry Fruits", IsActive = true },
                new SubCategoryMaster { Id = 113, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Flowers & Garlands", IsActive = true },
                new SubCategoryMaster { Id = 114, SubcategoryType = SubcategoryType.Product, CategoryMasterId = 1, Name = "Fruits for Offering", IsActive = true },

                // 2. Pooja Vessels & Utensils
                new SubCategoryMaster { Id = 201, CategoryMasterId = 2, Name = "Kalash", IsActive = true },
                new SubCategoryMaster { Id = 202, CategoryMasterId = 2, Name = "Pooja Thali", IsActive = true },
                new SubCategoryMaster { Id = 203, CategoryMasterId = 2, Name = "Bell (Ghanti)", IsActive = true },
                new SubCategoryMaster { Id = 204, CategoryMasterId = 2, Name = "Diya / Deepam", IsActive = true },
                new SubCategoryMaster { Id = 205, CategoryMasterId = 2, Name = "Aarti Stand", IsActive = true },
                new SubCategoryMaster { Id = 206, CategoryMasterId = 2, Name = "Panchapatra & Uddharani", IsActive = true },
                new SubCategoryMaster { Id = 207, CategoryMasterId = 2, Name = "Spoon, Bowls, Lota", IsActive = true },
                new SubCategoryMaster { Id = 208, CategoryMasterId = 2, Name = "Camphor Holder", IsActive = true },
                new SubCategoryMaster { Id = 209, CategoryMasterId = 2, Name = "Agarbatti Stand", IsActive = true },
                new SubCategoryMaster { Id = 210, CategoryMasterId = 2, Name = "Havan Kund", IsActive = true },
                new SubCategoryMaster { Id = 211, CategoryMasterId = 2, Name = "Ganga Jal Container", IsActive = true },

                // 3. Pooja Cloths & Decoration
                new SubCategoryMaster { Id = 301, CategoryMasterId = 3, Name = "Altar / Mandir Cloths", IsActive = true },
                new SubCategoryMaster { Id = 302, CategoryMasterId = 3, Name = "Deity Dresses (Vastra)", IsActive = true },
                new SubCategoryMaster { Id = 303, CategoryMasterId = 3, Name = "Chunri / Dupatta", IsActive = true },
                new SubCategoryMaster { Id = 304, CategoryMasterId = 3, Name = "Torans / Bandhanwars", IsActive = true },
                new SubCategoryMaster { Id = 305, CategoryMasterId = 3, Name = "Rangoli Powders", IsActive = true },
                new SubCategoryMaster { Id = 306, CategoryMasterId = 3, Name = "Garland / Mala Decorations", IsActive = true },

                // 4. Deities & Idols
                new SubCategoryMaster { Id = 401, CategoryMasterId = 4, Name = "Metal Idols", IsActive = true },
                new SubCategoryMaster { Id = 402, CategoryMasterId = 4, Name = "Marble Statues", IsActive = true },
                new SubCategoryMaster { Id = 403, CategoryMasterId = 4, Name = "Clay Idols", IsActive = true },
                new SubCategoryMaster { Id = 404, CategoryMasterId = 4, Name = "Frames & Photos", IsActive = true },

                // 5. Spiritual & Ritual Items
                new SubCategoryMaster { Id = 501, CategoryMasterId = 5, Name = "Rudraksha Mala", IsActive = true },
                new SubCategoryMaster { Id = 502, CategoryMasterId = 5, Name = "Tulsi Mala", IsActive = true },
                new SubCategoryMaster { Id = 503, CategoryMasterId = 5, Name = "Yantras", IsActive = true },
                new SubCategoryMaster { Id = 504, CategoryMasterId = 5, Name = "Conch Shell (Shankh)", IsActive = true },
                new SubCategoryMaster { Id = 505, CategoryMasterId = 5, Name = "Cow Dung Cakes", IsActive = true },
                new SubCategoryMaster { Id = 506, CategoryMasterId = 5, Name = "Sacred Threads", IsActive = true },
                new SubCategoryMaster { Id = 507, CategoryMasterId = 5, Name = "Navagraha Kits", IsActive = true },
                new SubCategoryMaster { Id = 508, CategoryMasterId = 5, Name = "Havan Samagri", IsActive = true },

                // 6. Pooja Kits / Combo Packs
                new SubCategoryMaster { Id = 601, CategoryMasterId = 6, Name = "Daily Pooja Kits", IsActive = true },
                new SubCategoryMaster { Id = 602, CategoryMasterId = 6, Name = "Festival Kits", IsActive = true },
                new SubCategoryMaster { Id = 603, CategoryMasterId = 6, Name = "Graha Pravesh Kit", IsActive = true },
                new SubCategoryMaster { Id = 604, CategoryMasterId = 6, Name = "Satyanarayan Pooja Kit", IsActive = true },
                new SubCategoryMaster { Id = 605, CategoryMasterId = 6, Name = "Rudra Abhishek Kit", IsActive = true },
                new SubCategoryMaster { Id = 606, CategoryMasterId = 6, Name = "Vastu Dosh Pooja Kit", IsActive = true },

                // 7. Holy Scriptures & Books
                new SubCategoryMaster { Id = 701, CategoryMasterId = 7, Name = "Bhagavad Gita", IsActive = true },
                new SubCategoryMaster { Id = 702, CategoryMasterId = 7, Name = "Ramayana", IsActive = true },
                new SubCategoryMaster { Id = 703, CategoryMasterId = 7, Name = "Pooja Vidhi Books", IsActive = true },
                new SubCategoryMaster { Id = 704, CategoryMasterId = 7, Name = "Panchang / Calendars", IsActive = true },

                // 8. Aroma & Fragrance Items
                new SubCategoryMaster { Id = 801, CategoryMasterId = 8, Name = "Attar / Natural Perfume", IsActive = true },
                new SubCategoryMaster { Id = 802, CategoryMasterId = 8, Name = "Sambrani", IsActive = true },
                new SubCategoryMaster { Id = 803, CategoryMasterId = 8, Name = "Essential Oils", IsActive = true },
                new SubCategoryMaster { Id = 804, CategoryMasterId = 8, Name = "Scented Candles", IsActive = true },

                // 9. Pooja Mandir Accessories
                new SubCategoryMaster { Id = 901, CategoryMasterId = 9, Name = "Mandirs (Temples)", IsActive = true },
                new SubCategoryMaster { Id = 902, CategoryMasterId = 9, Name = "Temple Bells", IsActive = true },
                new SubCategoryMaster { Id = 903, CategoryMasterId = 9, Name = "Pooja Mats / Aasan", IsActive = true },
                new SubCategoryMaster { Id = 904, CategoryMasterId = 9, Name = "Idol Platforms", IsActive = true },

                // 10. Miscellaneous
                new SubCategoryMaster { Id = 1001, CategoryMasterId = 10, Name = "Donation Boxes", IsActive = true },
                new SubCategoryMaster { Id = 1002, CategoryMasterId = 10, Name = "Copper / Silver Coins", IsActive = true },
                new SubCategoryMaster { Id = 1003, CategoryMasterId = 10, Name = "Holy Soil", IsActive = true },
                new SubCategoryMaster { Id = 1004, CategoryMasterId = 10, Name = "Sacred Plants & Leaves", IsActive = true },

                // === Basic/Common Poojas ===
                new SubCategoryMaster { Id = 1101, CategoryMasterId = 11, Name = "Archana / Name Recital", Description = "Recitation of devotee's name & prayers", IsActive = true },
                new SubCategoryMaster { Id = 1102, CategoryMasterId = 11, Name = "Abhishekam", Description = "Ritual bathing of deity with sacred substances", IsActive = true },
                new SubCategoryMaster { Id = 1103, CategoryMasterId = 11, Name = "Alankaram / Decoration", Description = "Decoration of deity with flowers and clothes", IsActive = true },
                new SubCategoryMaster { Id = 1104, CategoryMasterId = 11, Name = "Aarti / Deepa Aradhana", Description = "Offering of light during pooja", IsActive = true },
                new SubCategoryMaster { Id = 1105, CategoryMasterId = 11, Name = "Nitya Pooja", Description = "Daily temple poojas (morning, afternoon, evening)", IsActive = true },
                new SubCategoryMaster { Id = 1106, CategoryMasterId = 11, Name = "Annadanam (Food Donation)", Description = "Offering or sponsoring food distribution", IsActive = true },

                // === Fire Rituals (Homam/Havan) ===
                new SubCategoryMaster { Id = 1107, CategoryMasterId = 11, Name = "Homam / Havan", Description = "Sacred fire rituals", IsActive = true },
                new SubCategoryMaster { Id = 1108, CategoryMasterId = 11, Name = "Rudrabhishekam", Description = "Special abhishekam with Rudra Mantras", IsActive = true },
                new SubCategoryMaster { Id = 1109, CategoryMasterId = 11, Name = "Sahasranama Pooja", Description = "Chanting 1000 names of the deity", IsActive = true },
                new SubCategoryMaster { Id = 1110, CategoryMasterId = 11, Name = "Navagraha Shanti", Description = "Pooja to appease nine planets", IsActive = true },
                new SubCategoryMaster { Id = 1111, CategoryMasterId = 11, Name = "Navagraha Homa", Description = "Fire ritual for planetary pacification", IsActive = true },
                new SubCategoryMaster { Id = 1112, CategoryMasterId = 11, Name = "Chandi Homam", Description = "Fire ritual invoking Goddess Chandi", IsActive = true },
                new SubCategoryMaster { Id = 1113, CategoryMasterId = 11, Name = "Ganapathi Homam", Description = "Fire ritual dedicated to Lord Ganesha", IsActive = true },
                new SubCategoryMaster { Id = 1114, CategoryMasterId = 11, Name = "Ayush Homam", Description = "Fire ritual for longevity and health", IsActive = true },

                // === Festival & Special Poojas ===
                new SubCategoryMaster { Id = 1116, CategoryMasterId = 11, Name = "Kalyanotsavam / Divine Wedding", Description = "Symbolic wedding ceremonies of deities", IsActive = true },
                new SubCategoryMaster { Id = 1117, CategoryMasterId = 11, Name = "Sri Satyanarayana Pooja", Description = "Pooja for prosperity and well-being", IsActive = true },
                new SubCategoryMaster { Id = 1118, CategoryMasterId = 11, Name = "Durga Pooja", Description = "Special worship during Navratri", IsActive = true },
                new SubCategoryMaster { Id = 1119, CategoryMasterId = 11, Name = "Lakshmi Pooja", Description = "Worship of Goddess Lakshmi for wealth", IsActive = true },
                new SubCategoryMaster { Id = 1120, CategoryMasterId = 11, Name = "Saraswati Pooja", Description = "Worship of Goddess Saraswati for knowledge", IsActive = true },
                new SubCategoryMaster { Id = 1121, CategoryMasterId = 11, Name = "Maha Mrityunjaya Pooja", Description = "Healing and longevity pooja invoking Shiva", IsActive = true },
                new SubCategoryMaster { Id = 1122, CategoryMasterId = 11, Name = "Navaratri Special Poojas", Description = "Series of poojas during Navaratri festival", IsActive = true },
                new SubCategoryMaster { Id = 1123, CategoryMasterId = 11, Name = "Satyanarayan Katha", Description = "Storytelling and pooja for peace", IsActive = true },

                // === Donations & Sponsorships ===
                new SubCategoryMaster { Id = 1124, CategoryMasterId = 11, Name = "Temple Maintenance Donation", Description = "Donations for temple upkeep", IsActive = true },
                new SubCategoryMaster { Id = 1125, CategoryMasterId = 11, Name = "Gopuram / Mandapam Donation", Description = "Donations for temple structures", IsActive = true },
                new SubCategoryMaster { Id = 1126, CategoryMasterId = 11, Name = "Prasad Seva", Description = "Sponsoring distribution of prasad", IsActive = true },
                new SubCategoryMaster { Id = 1127, CategoryMasterId = 11, Name = "Vastra Daanam", Description = "Offering clothes to the deity", IsActive = true },

                // === Other Rituals & Worships ===
                new SubCategoryMaster { Id = 1128, CategoryMasterId = 11, Name = "Vahana Seva", Description = "Deity procession on sacred vehicles", IsActive = true },
                new SubCategoryMaster { Id = 1129, CategoryMasterId = 11, Name = "Pushpa Alankaram", Description = "Offering flowers or garlands", IsActive = true },
                new SubCategoryMaster { Id = 1130, CategoryMasterId = 11, Name = "Bhairava Pooja", Description = "Worship of Lord Bhairava for protection", IsActive = true },
                new SubCategoryMaster { Id = 1131, CategoryMasterId = 11, Name = "Gopuja / Cow Worship", Description = "Worship of sacred cows at temple", IsActive = true },
                new SubCategoryMaster { Id = 1132, CategoryMasterId = 11, Name = "Sarpa Dosha Pooja", Description = "Rituals for snake dosha relief", IsActive = true },
                new SubCategoryMaster { Id = 1133, CategoryMasterId = 11, Name = "Guru Pooja", Description = "Worship of spiritual teachers or gurus", IsActive = true },
                new SubCategoryMaster { Id = 1134, CategoryMasterId = 11, Name = "Kalabhairava Pooja", Description = "Worship of Lord Kalabhairava, temple guardian deity", IsActive = true },
                new SubCategoryMaster { Id = 1135, CategoryMasterId = 11, Name = "Ashta Lakshmi Pooja", Description = "Worship of eight forms of Goddess Lakshmi", IsActive = true },
                new SubCategoryMaster { Id = 1136, CategoryMasterId = 11, Name = "Dhanvantri Pooja", Description = "Worship of Lord Dhanvantri for health", IsActive = true },
                new SubCategoryMaster { Id = 1137, CategoryMasterId = 11, Name = "Vastu Shanti", Description = "Rituals to remove vastu dosha", IsActive = true },
                new SubCategoryMaster { Id = 1138, CategoryMasterId = 11, Name = "Temple Entry / VIP Darshan", Description = "Special access tickets to temple darshan", IsActive = true },
                // === Specific Pooja ===
                new SubCategoryMaster { Id = 1139, CategoryMasterId = 11, Name = "Ganapati Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1140, CategoryMasterId = 11, Name = "Shiva Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1141, CategoryMasterId = 11, Name = "Vishnu Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1142, CategoryMasterId = 11, Name = "Hanuman Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1143, CategoryMasterId = 11, Name = "Satyanarayan Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1144, CategoryMasterId = 11, Name = "Navratri Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1145, CategoryMasterId = 11, Name = "Ganesh Chaturthi Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1146, CategoryMasterId = 11, Name = "Janmashtami Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1147, CategoryMasterId = 11, Name = "Ram Navami Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1148, CategoryMasterId = 11, Name = "Karva Chauth Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1149, CategoryMasterId = 11, Name = "Govardhan Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1150, CategoryMasterId = 11, Name = "Vasant Panchami Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1151, CategoryMasterId = 11, Name = "Chaitra Navratri Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1152, CategoryMasterId = 11, Name = "Griha Pravesh Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1153, CategoryMasterId = 11, Name = "Bhumi Poojan", IsActive = true },
                new SubCategoryMaster { Id = 1154, CategoryMasterId = 11, Name = "Vastu Shanti Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1155, CategoryMasterId = 11, Name = "Wedding Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1156, CategoryMasterId = 11, Name = "Namkaran Sanskar", IsActive = true },
                new SubCategoryMaster { Id = 1157, CategoryMasterId = 11, Name = "Annaprashan", IsActive = true },
                new SubCategoryMaster { Id = 1158, CategoryMasterId = 11, Name = "Mundan", IsActive = true },
                new SubCategoryMaster { Id = 1159, CategoryMasterId = 11, Name = "Upanayanam", IsActive = true },
                new SubCategoryMaster { Id = 1160, CategoryMasterId = 11, Name = "Seemantham", IsActive = true },
                new SubCategoryMaster { Id = 1161, CategoryMasterId = 11, Name = "Ganapati Homam", IsActive = true },
                new SubCategoryMaster { Id = 1162, CategoryMasterId = 11, Name = "Navagraha Homam", IsActive = true },
                new SubCategoryMaster { Id = 1163, CategoryMasterId = 11, Name = "Rudra Homam", IsActive = true },
                new SubCategoryMaster { Id = 1164, CategoryMasterId = 11, Name = "Chandi Homam", IsActive = true },
                new SubCategoryMaster { Id = 1165, CategoryMasterId = 11, Name = "Mrityunjaya Homam", IsActive = true },
                new SubCategoryMaster { Id = 1166, CategoryMasterId = 11, Name = "Sudarshana Homam", IsActive = true },
                new SubCategoryMaster { Id = 1167, CategoryMasterId = 11, Name = "Dhanvantri Homam", IsActive = true },
                new SubCategoryMaster { Id = 1168, CategoryMasterId = 11, Name = "Lakshmi Kubera Homam", IsActive = true },
                new SubCategoryMaster { Id = 1169, CategoryMasterId = 11, Name = "Sarpa Dosha Nivaran Homam", IsActive = true },
                new SubCategoryMaster { Id = 1170, CategoryMasterId = 11, Name = "Kala Sarpa Shanti", IsActive = true },
                new SubCategoryMaster { Id = 1171, CategoryMasterId = 11, Name = "Graha Shanti Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1172, CategoryMasterId = 11, Name = "Shani Shanti Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1173, CategoryMasterId = 11, Name = "Rahu-Ketu Shanti Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1174, CategoryMasterId = 11, Name = "Mangal Dosha Nivaran", IsActive = true },
                new SubCategoryMaster { Id = 1175, CategoryMasterId = 11, Name = "Pitru Dosha Nivaran", IsActive = true },
                new SubCategoryMaster { Id = 1176, CategoryMasterId = 11, Name = "Naga Pratishtha Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1177, CategoryMasterId = 11, Name = "Ketu Shanti Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1178, CategoryMasterId = 11, Name = "Maha Mrityunjaya Jap", IsActive = true },
                new SubCategoryMaster { Id = 1179, CategoryMasterId = 11, Name = "Durga Saptashati Parayan", IsActive = true },
                new SubCategoryMaster { Id = 1180, CategoryMasterId = 11, Name = "Vishnu Sahasranama Parayan", IsActive = true },
                new SubCategoryMaster { Id = 1181, CategoryMasterId = 11, Name = "Lalitha Sahasranama Archana", IsActive = true },
                new SubCategoryMaster { Id = 1182, CategoryMasterId = 11, Name = "Sri Chakra Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1183, CategoryMasterId = 11, Name = "Ashtalakshmi Pooja", IsActive = true },
                new SubCategoryMaster { Id = 1184, CategoryMasterId = 11, Name = "Hanuman Chalisa Parayan", IsActive = true },
                new SubCategoryMaster { Id = 1185, CategoryMasterId = 11, Name = "Sunderkand Path", IsActive = true },
                new SubCategoryMaster { Id = 1186, CategoryMasterId = 11, Name = "Bhagavad Gita Path", IsActive = true },

                // Astrology (CategoryId = 12)
                new SubCategoryMaster { Id = 1200, CategoryMasterId = 12, Name = "Vedic Astrology", Description = "Traditional Indian astrology system", IsActive = true },
                new SubCategoryMaster { Id = 1201, CategoryMasterId = 12, Name = "Western Astrology", Description = "Western zodiac signs and horoscopes", IsActive = true },
                new SubCategoryMaster { Id = 1202, CategoryMasterId = 12, Name = "Chinese Astrology", Description = "Chinese zodiac and astrology system", IsActive = true },
                new SubCategoryMaster { Id = 1203, CategoryMasterId = 12, Name = "Daily/Weekly/Monthly/Yearly Horoscopes", Description = "Periodic astrological predictions", IsActive = true },
                new SubCategoryMaster { Id = 1204, CategoryMasterId = 12, Name = "Natal/Birth Chart Reading", Description = "Detailed natal chart reading", IsActive = true },
                new SubCategoryMaster { Id = 1205, CategoryMasterId = 12, Name = "Lagna & Rashi Analysis", Description = "Ascendant and zodiac sign analysis", IsActive = true },
                new SubCategoryMaster { Id = 1206, CategoryMasterId = 12, Name = "Planetary Transits (Gochar)", Description = "Analysis of planet movements", IsActive = true },
                new SubCategoryMaster { Id = 1207, CategoryMasterId = 12, Name = "Dasha Predictions", Description = "Planetary period predictions", IsActive = true },
                new SubCategoryMaster { Id = 1208, CategoryMasterId = 12, Name = "Kundli Matching / Compatibility", Description = "Marriage and relationship compatibility", IsActive = true },
                new SubCategoryMaster { Id = 1209, CategoryMasterId = 12, Name = "Career & Finance Predictions", Description = "Professional and financial insights", IsActive = true },
                new SubCategoryMaster { Id = 1210, CategoryMasterId = 12, Name = "Love & Marriage Astrology", Description = "Astrology focused on relationships", IsActive = true },
                new SubCategoryMaster { Id = 1211, CategoryMasterId = 12, Name = "Health Astrology", Description = "Predictions related to health", IsActive = true },
                new SubCategoryMaster { Id = 1212, CategoryMasterId = 12, Name = "Childbirth & Progeny Astrology", Description = "Family and childbirth insights", IsActive = true },
                new SubCategoryMaster { Id = 1213, CategoryMasterId = 12, Name = "Gemstone Recommendations", Description = "Gem recommendations for astrological benefits", IsActive = true },
                new SubCategoryMaster { Id = 1214, CategoryMasterId = 12, Name = "Remedies & Upayas (Astrological Solutions)", Description = "Solutions to astrological problems", IsActive = true },
                new SubCategoryMaster { Id = 1215, CategoryMasterId = 12, Name = "Muhurat (Auspicious Timing)", Description = "Finding auspicious time for events", IsActive = true },
                new SubCategoryMaster { Id = 1216, CategoryMasterId = 12, Name = "Mangal Dosha / Kaal Sarp Dosha Analysis", Description = "Dosha related analysis and remedies", IsActive = true },


                // Numerology (CategoryMasterId = 13)
                new SubCategoryMaster { Id = 1301, CategoryMasterId = 13, Name = "Numerology Reading", Description = "Detailed reading based on numbers", IsActive = true },
                new SubCategoryMaster { Id = 1302, CategoryMasterId = 13, Name = "Lucky Number Prediction", Description = "Your most auspicious number", IsActive = true },
                new SubCategoryMaster { Id = 1303, CategoryMasterId = 13, Name = "Name Numerology / Name Correction", Description = "Name analysis and correction", IsActive = true },
                new SubCategoryMaster { Id = 1304, CategoryMasterId = 13, Name = "Birth Date Numerology", Description = "Numerology from birth date", IsActive = true },
                new SubCategoryMaster { Id = 1305, CategoryMasterId = 13, Name = "Life Path Number Analysis", Description = "Life journey based on birth date", IsActive = true },
                new SubCategoryMaster { Id = 1306, CategoryMasterId = 13, Name = "Destiny Number", Description = "Numerology of your destiny", IsActive = true },
                new SubCategoryMaster { Id = 1307, CategoryMasterId = 13, Name = "Expression Number", Description = "Numerology of your expression", IsActive = true },
                new SubCategoryMaster { Id = 1308, CategoryMasterId = 13, Name = "Soul Urge Number", Description = "Deep inner desires through numbers", IsActive = true },
                new SubCategoryMaster { Id = 1309, CategoryMasterId = 13, Name = "Compatibility Based on Numbers", Description = "Relationship compatibility numerology", IsActive = true },
                new SubCategoryMaster { Id = 1310, CategoryMasterId = 13, Name = "Mobile Number Numerology", Description = "Numerology for mobile numbers", IsActive = true },
                new SubCategoryMaster { Id = 1311, CategoryMasterId = 13, Name = "Business Name Numerology", Description = "Numerology for business names", IsActive = true },
                new SubCategoryMaster { Id = 1312, CategoryMasterId = 13, Name = "Vehicle Number Analysis", Description = "Numerology for vehicle numbers", IsActive = true },
                new SubCategoryMaster { Id = 1313, CategoryMasterId = 13, Name = "Personal Year Forecast", Description = "Yearly numerology forecast", IsActive = true },
                new SubCategoryMaster { Id = 1314, CategoryMasterId = 13, Name = "Numerology Remedies", Description = "Solutions and remedies in numerology", IsActive = true },

                // Tarot (CategoryMasterId = 14)
                new SubCategoryMaster { Id = 1401, CategoryMasterId = 14, Name = "General Tarot Reading", Description = "Overall guidance through tarot", IsActive = true },
                new SubCategoryMaster { Id = 1402, CategoryMasterId = 14, Name = "Love & Relationship Tarot", Description = "Insights into love life", IsActive = true },
                new SubCategoryMaster { Id = 1403, CategoryMasterId = 14, Name = "Career & Finance Tarot", Description = "Career and financial advice", IsActive = true },
                new SubCategoryMaster { Id = 1404, CategoryMasterId = 14, Name = "Health Tarot", Description = "Health-related tarot readings", IsActive = true },
                new SubCategoryMaster { Id = 1405, CategoryMasterId = 14, Name = "Yes/No Tarot Reading", Description = "Simple yes/no answers", IsActive = true },
                new SubCategoryMaster { Id = 1406, CategoryMasterId = 14, Name = "Daily/Weekly/Monthly Tarot", Description = "Periodic tarot readings", IsActive = true },
                new SubCategoryMaster { Id = 1407, CategoryMasterId = 14, Name = "Celtic Cross Spread", Description = "Detailed tarot spread", IsActive = true },
                new SubCategoryMaster { Id = 1408, CategoryMasterId = 14, Name = "3-Card Spread (Past/Present/Future)", Description = "Quick three-card reading", IsActive = true },
                new SubCategoryMaster { Id = 1409, CategoryMasterId = 14, Name = "Angel Card Reading", Description = "Guidance through angel cards", IsActive = true },
                new SubCategoryMaster { Id = 1410, CategoryMasterId = 14, Name = "Oracle Card Reading", Description = "Intuitive oracle card readings", IsActive = true },
                new SubCategoryMaster { Id = 1411, CategoryMasterId = 14, Name = "Tarot for Decision-Making", Description = "Using tarot to guide choices", IsActive = true },

                // Palmistry (CategoryMasterId = 15)
                new SubCategoryMaster { Id = 1501, CategoryMasterId = 15, Name = "Full Palm Reading", Description = "Complete palm analysis", IsActive = true },
                new SubCategoryMaster { Id = 1502, CategoryMasterId = 15, Name = "Life Line Reading", Description = "Interpretation of life line", IsActive = true },
                new SubCategoryMaster { Id = 1503, CategoryMasterId = 15, Name = "Heart Line Reading", Description = "Emotional and relationship insights", IsActive = true },
                new SubCategoryMaster { Id = 1504, CategoryMasterId = 15, Name = "Head Line Analysis", Description = "Intellectual and mental traits", IsActive = true },
                new SubCategoryMaster { Id = 1505, CategoryMasterId = 15, Name = "Fate Line Interpretation", Description = "Career and destiny line", IsActive = true },
                new SubCategoryMaster { Id = 1506, CategoryMasterId = 15, Name = "Marriage Line Reading", Description = "Insights on marriage and relationships", IsActive = true },
                new SubCategoryMaster { Id = 1507, CategoryMasterId = 15, Name = "Career & Finance Prediction from Palm", Description = "Palm-based career guidance", IsActive = true },
                new SubCategoryMaster { Id = 1508, CategoryMasterId = 15, Name = "Mounts & Finger Analysis", Description = "Detailed mounts and finger study", IsActive = true },
                new SubCategoryMaster { Id = 1509, CategoryMasterId = 15, Name = "Palm Reading via Photos (Online)", Description = "Remote palmistry service", IsActive = true },

                // Psychic (CategoryMasterId = 16)
                new SubCategoryMaster { Id = 1601, CategoryMasterId = 16, Name = "Psychic Reading", Description = "Intuitive psychic readings", IsActive = true },
                new SubCategoryMaster { Id = 1602, CategoryMasterId = 16, Name = "Intuitive Guidance", Description = "Spiritual and intuitive advice", IsActive = true },
                new SubCategoryMaster { Id = 1603, CategoryMasterId = 16, Name = "Clairvoyant Readings", Description = "Vision-based psychic readings", IsActive = true },
                new SubCategoryMaster { Id = 1604, CategoryMasterId = 16, Name = "Clairaudience / Clairsentience", Description = "Hearing and feeling psychic abilities", IsActive = true },
                new SubCategoryMaster { Id = 1605, CategoryMasterId = 16, Name = "Mediumship / Spirit Communication", Description = "Communicating with spirits", IsActive = true },
                new SubCategoryMaster { Id = 1606, CategoryMasterId = 16, Name = "Past Life Regression Reading", Description = "Exploring past lives", IsActive = true },
                new SubCategoryMaster { Id = 1607, CategoryMasterId = 16, Name = "Future Predictions", Description = "Forecasting future events", IsActive = true },
                new SubCategoryMaster { Id = 1608, CategoryMasterId = 16, Name = "Dream Interpretation", Description = "Analyzing dreams and meanings", IsActive = true },
                new SubCategoryMaster { Id = 1609, CategoryMasterId = 16, Name = "Aura Reading", Description = "Reading spiritual energy fields", IsActive = true },
                new SubCategoryMaster { Id = 1610, CategoryMasterId = 16, Name = "Akashic Records Reading", Description = "Accessing spiritual records", IsActive = true },

                // Healing (CategoryMasterId = 17)
                new SubCategoryMaster { Id = 1701, CategoryMasterId = 17, Name = "Reiki Healing", Description = "Energy healing technique", IsActive = true },
                new SubCategoryMaster { Id = 1702, CategoryMasterId = 17, Name = "Pranic Healing", Description = "Energy cleansing and healing", IsActive = true },
                new SubCategoryMaster { Id = 1703, CategoryMasterId = 17, Name = "Crystal Healing", Description = "Healing with crystals and stones", IsActive = true },
                new SubCategoryMaster { Id = 1704, CategoryMasterId = 17, Name = "Sound Healing", Description = "Healing using sound frequencies", IsActive = true },
                new SubCategoryMaster { Id = 1705, CategoryMasterId = 17, Name = "Chakra Balancing", Description = "Balancing energy centers", IsActive = true },
                new SubCategoryMaster { Id = 1706, CategoryMasterId = 17, Name = "Energy Cleansing", Description = "Removing negative energies", IsActive = true },
                new SubCategoryMaster { Id = 1707, CategoryMasterId = 17, Name = "Emotional Healing", Description = "Healing emotional wounds", IsActive = true },
                new SubCategoryMaster { Id = 1708, CategoryMasterId = 17, Name = "Spiritual Healing", Description = "Healing on a spiritual level", IsActive = true },
                new SubCategoryMaster { Id = 1709, CategoryMasterId = 17, Name = "Aura Cleansing", Description = "Cleaning the aura", IsActive = true },
                new SubCategoryMaster { Id = 1710, CategoryMasterId = 17, Name = "Meditation & Guided Healing", Description = "Meditation techniques and guidance", IsActive = true },
                new SubCategoryMaster { Id = 1711, CategoryMasterId = 17, Name = "Distance Healing", Description = "Remote energy healing", IsActive = true },

                // Vastu (CategoryMasterId = 18)
                new SubCategoryMaster { Id = 1801, CategoryMasterId = 18, Name = "Residential Vastu Consultation", Description = "Vastu for homes", IsActive = true },
                new SubCategoryMaster { Id = 1802, CategoryMasterId = 18, Name = "Commercial Vastu (Shops, Offices)", Description = "Vastu for business spaces", IsActive = true },
                new SubCategoryMaster { Id = 1803, CategoryMasterId = 18, Name = "Industrial Vastu", Description = "Vastu for factories and industries", IsActive = true },
                new SubCategoryMaster { Id = 1804, CategoryMasterId = 18, Name = "Vastu for Plots & New Constructions", Description = "Planning based on vastu", IsActive = true },
                new SubCategoryMaster { Id = 1805, CategoryMasterId = 18, Name = "Vastu Remedies (Without Demolition)", Description = "Fix vastu issues without construction", IsActive = true },
                new SubCategoryMaster { Id = 1806, CategoryMasterId = 18, Name = "Room-wise Vastu (Kitchen, Bedroom, etc.)", Description = "Vastu tips for individual rooms", IsActive = true },
                new SubCategoryMaster { Id = 1807, CategoryMasterId = 18, Name = "Online Vastu Consultation", Description = "Virtual vastu consultations", IsActive = true },
                new SubCategoryMaster { Id = 1808, CategoryMasterId = 18, Name = "Vastu for Peace & Prosperity", Description = "Enhance wellbeing through vastu", IsActive = true },

                // Consultation (CategoryMasterId = 19)
                new SubCategoryMaster { Id = 1901, CategoryMasterId = 19, Name = "Personal Astrology Consultation", Description = "One-on-one astrology sessions", IsActive = true },
                new SubCategoryMaster { Id = 1902, CategoryMasterId = 19, Name = "Numerology Consultation", Description = "Personal numerology guidance", IsActive = true },
                new SubCategoryMaster { Id = 1903, CategoryMasterId = 19, Name = "Tarot Consultation", Description = "Tarot reading consultations", IsActive = true },
                new SubCategoryMaster { Id = 1904, CategoryMasterId = 19, Name = "Vastu Consultation", Description = "Vastu expert sessions", IsActive = true },
                new SubCategoryMaster { Id = 1905, CategoryMasterId = 19, Name = "Palmistry Session", Description = "Palm reading consultations", IsActive = true },
                new SubCategoryMaster { Id = 1906, CategoryMasterId = 19, Name = "Relationship Counselling", Description = "Guidance for relationships", IsActive = true },
                new SubCategoryMaster { Id = 1907, CategoryMasterId = 19, Name = "Career Guidance", Description = "Professional advice sessions", IsActive = true },
                new SubCategoryMaster { Id = 1908, CategoryMasterId = 19, Name = "Business Consultation", Description = "Business-related guidance", IsActive = true },
                new SubCategoryMaster { Id = 1909, CategoryMasterId = 19, Name = "Health-Related Consultation", Description = "Health advice and sessions", IsActive = true },
                new SubCategoryMaster { Id = 1910, CategoryMasterId = 19, Name = "Kundli Interpretation Session", Description = "In-depth kundli reading", IsActive = true },
                new SubCategoryMaster { Id = 1911, CategoryMasterId = 19, Name = "Online / Phone / Video Consultations", Description = "Virtual consultation sessions", IsActive = true },

                // Rituals (CategoryMasterId = 20)
                new SubCategoryMaster { Id = 2001, CategoryMasterId = 20, Name = "Vedic Puja Services (Ganesh, Lakshmi, etc.)", Description = "Traditional pujas for deities", IsActive = true },
                new SubCategoryMaster { Id = 2002, CategoryMasterId = 20, Name = "Havan / Homa (Navagraha, Rudra, etc.)", Description = "Fire rituals for wellbeing", IsActive = true },
                new SubCategoryMaster { Id = 2003, CategoryMasterId = 20, Name = "Graha Shanti Puja", Description = "Rituals for planetary peace", IsActive = true },
                new SubCategoryMaster { Id = 2004, CategoryMasterId = 20, Name = "Mangal Dosha Nivaran Puja", Description = "Remedy puja for mangal dosha", IsActive = true },
                new SubCategoryMaster { Id = 2005, CategoryMasterId = 20, Name = "Kaal Sarp Dosha Puja", Description = "Rituals for kaal sarp dosha", IsActive = true },
                new SubCategoryMaster { Id = 2006, CategoryMasterId = 20, Name = "Marriage Puja & Rituals", Description = "Ceremonies for marriage", IsActive = true },
                new SubCategoryMaster { Id = 2007, CategoryMasterId = 20, Name = "Black Magic Removal", Description = "Rituals to remove black magic", IsActive = true },
                new SubCategoryMaster { Id = 2008, CategoryMasterId = 20, Name = "Evil Eye Protection Rituals", Description = "Protection from negative energy", IsActive = true },
                new SubCategoryMaster { Id = 2009, CategoryMasterId = 20, Name = "Prosperity Rituals", Description = "Rituals to attract wealth", IsActive = true },
                new SubCategoryMaster { Id = 2010, CategoryMasterId = 20, Name = "Health & Healing Rituals", Description = "Rituals for health and healing", IsActive = true },
                new SubCategoryMaster { Id = 2011, CategoryMasterId = 20, Name = "Custom Rituals (Based on Horoscope)", Description = "Personalized rituals", IsActive = true },

                // Kundli Services (CategoryMasterId = 21)
                new SubCategoryMaster { Id = 2101, CategoryMasterId = 21, Name = "Kundli Generation (Online/Offline)", Description = "Creation of birth charts", IsActive = true },
                new SubCategoryMaster { Id = 2102, CategoryMasterId = 21, Name = "Janam Kundli in Hindi/English", Description = "Birth charts in preferred language", IsActive = true },
                new SubCategoryMaster { Id = 2103, CategoryMasterId = 21, Name = "Kundli Matching (Gun Milan)", Description = "Marriage compatibility", IsActive = true },
                new SubCategoryMaster { Id = 2104, CategoryMasterId = 21, Name = "Kundli Dosh Analysis", Description = "Dosha analysis in kundli", IsActive = true },
                new SubCategoryMaster { Id = 2105, CategoryMasterId = 21, Name = "Mangal Dosh", Description = "Specific mangal dosh analysis", IsActive = true },
                new SubCategoryMaster { Id = 2106, CategoryMasterId = 21, Name = "Kaal Sarp Dosh", Description = "Specific kaal sarp dosh analysis", IsActive = true },
                new SubCategoryMaster { Id = 2107, CategoryMasterId = 21, Name = "Pitra Dosh", Description = "Pitra dosh analysis and remedies", IsActive = true },
                new SubCategoryMaster { Id = 2108, CategoryMasterId = 21, Name = "Nadi Dosh", Description = "Nadi dosh insights", IsActive = true },
                new SubCategoryMaster { Id = 2109, CategoryMasterId = 21, Name = "Personalized Kundli Report", Description = "Customized birth chart reports", IsActive = true },
                new SubCategoryMaster { Id = 2110, CategoryMasterId = 21, Name = "Remedies Based on Kundli", Description = "Solutions based on kundli findings", IsActive = true },
                new SubCategoryMaster { Id = 2111, CategoryMasterId = 21, Name = "Career / Marriage / Finance from Kundli", Description = "Focused predictions from kundli", IsActive = true },
                new SubCategoryMaster { Id = 2112, CategoryMasterId = 21, Name = "Printed Kundli Booklets / PDFs", Description = "Physical or digital kundli copies", IsActive = true },

                // Priest (CategoryMasterId = 22)
                new SubCategoryMaster { Id = 2201, CategoryMasterId = 22, SubcategoryType = SubcategoryType.Priest, Name = "North Indian Pandit", Description = "Book experienced North Indian pandits for rituals and ceremonies", IsActive = true },
                new SubCategoryMaster { Id = 2202, CategoryMasterId = 22, SubcategoryType = SubcategoryType.Priest, Name = "South Indian Pandit", Description = "Hire South Indian priests for traditional ceremonies", IsActive = true },
                new SubCategoryMaster { Id = 2203, CategoryMasterId = 22, SubcategoryType = SubcategoryType.Priest, Name = "Telugu / Kannada / Tamil Priest", Description = "Language-specific priest services", IsActive = true },
                new SubCategoryMaster { Id = 2204, CategoryMasterId = 22, SubcategoryType = SubcategoryType.Priest, Name = "Purohit for Griha Pravesh", Description = "Priest services for housewarming rituals", IsActive = true },
                new SubCategoryMaster { Id = 2205, CategoryMasterId = 22, SubcategoryType = SubcategoryType.Priest, Name = "Priest for Marriage Ceremony", Description = "Book priests for Hindu weddings", IsActive = true },
                new SubCategoryMaster { Id = 2206, CategoryMasterId = 22, SubcategoryType = SubcategoryType.Priest, Name = "Priest for Antim Sanskar", Description = "Last rites priest services", IsActive = true },

                // Temple (CategoryMasterId = 22)
                new SubCategoryMaster { Id = 2301, SubcategoryType = SubcategoryType.Temple, CategoryMasterId = 23, Name = "Temple Darshan Booking", Description = "Book priority darshan slots at temples", IsActive = true },
                new SubCategoryMaster { Id = 2302, SubcategoryType = SubcategoryType.Temple, CategoryMasterId = 23, Name = "Online Temple Puja", Description = "Remote puja performed at temples", IsActive = true },
                new SubCategoryMaster { Id = 2303, SubcategoryType = SubcategoryType.Temple, CategoryMasterId = 23, Name = "Archana / Abhishekam Services", Description = "Book rituals in temple on your behalf", IsActive = true },
                new SubCategoryMaster { Id = 2304, SubcategoryType = SubcategoryType.Temple, CategoryMasterId = 23, Name = "Temple Prasadam Delivery", Description = "Get temple prasadam delivered to your home", IsActive = true },
                new SubCategoryMaster { Id = 2305, SubcategoryType = SubcategoryType.Temple, CategoryMasterId = 23, Name = "Donation to Temples", Description = "Make donations to listed temples", IsActive = true },

                // KathaVachak, Satsang, jagran (CategoryMasterId = 23)
                new SubCategoryMaster { Id = 2401, CategoryMasterId = 24, Name = "Bhajan Mandali Booking", Description = "Book a devotional bhajan group for events", IsActive = true },
                new SubCategoryMaster { Id = 2402, CategoryMasterId = 24, Name = "Jagran Event", Description = "Book Jagran for religious gatherings", IsActive = true },
                new SubCategoryMaster { Id = 2403, CategoryMasterId = 24, Name = "Satsang Services", Description = "Organize spiritual discourses and satsangs", IsActive = true },
                new SubCategoryMaster { Id = 2404, CategoryMasterId = 24, Name = "Kathavachak Booking", Description = "Hire experienced kathavachaks for spiritual storytelling", IsActive = true },
                new SubCategoryMaster { Id = 2405, CategoryMasterId = 24, Name = "Ramayan / Bhagwat Katha", Description = "Organize Ramayan or Bhagwat Katha events", IsActive = true },
                new SubCategoryMaster { Id = 2406, CategoryMasterId = 24, Name = "Sai Sandhya", Description = "Host a devotional Sai Baba event", IsActive = true },


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
            string newImageUrl = "https://www.pujasthan.com/wp-content/uploads/2023/08/Puja-Samagri-Online-3.png";

            categories.ForEach(c => c.ImageUrl = newImageUrl);

            SeedCatalogDto seedCatalogDto = new SeedCatalogDto
            {
                CategoryMasters = categories,
                SubCategoryMasters = subcategories,
                PoojaKitItems = poojaKitItems
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