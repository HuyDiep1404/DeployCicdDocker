using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AspNetDistributedCaching.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
