namespace Catalog.Domain.Enums
{
    public enum SubcategoryType
    {
        Unknown = 0,

        // For temple, home, and astrology services
        Service = 1,
        Pooja = 2,
        Aarti = 3,
        Donation = 4,
        Prasad = 5,
        Bhajan = 6,
        Bhandara = 7,
        Astrology = 8,

        // For product categories
        Product = 10,
        Kit = 11,

        // Special classifications
        KitGroup = 20,
    }

}