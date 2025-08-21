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
        Priest = 8,
        Temple = 9,
        Astrology = 10,

        // For product categories
        Product = 11,
        Kit = 12,

        // Special classifications
        KitGroup = 30,
    }

}