using MongoDB.Driver;

namespace Shared.Domain.Util
{
    /// <summary>
    /// Internal miscellaneous utility functions.
    /// </summary>
    public static class MongoCollectionUtils
    {
        public static string? GetCollectionName(Type collection)
        {
            return ((MongoCollectionAttribute)collection
                     .GetCustomAttributes(typeof(MongoCollectionAttribute), true)
                     .FirstOrDefault()!).CollectionName;
        }

        public static string? GetDataBaseName(Type collection)
        {
            return ((MongoCollectionAttribute)collection
                         .GetCustomAttributes(typeof(MongoCollectionAttribute), true)
                         .FirstOrDefault()!).DataBaseName;
        }
    }
}