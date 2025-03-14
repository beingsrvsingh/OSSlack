namespace Shared.Domain.Util
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MongoCollectionAttribute : Attribute
    {
        public string CollectionName { get; }
        public string DataBaseName { get; }
        public MongoCollectionAttribute(string collectionName, string dataBaseName = null)
        {
            CollectionName = collectionName;
            DataBaseName = dataBaseName;
        }
    }
}