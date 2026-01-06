using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Shared
{
    public class Food
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }
        public string? PictureBase64 { get; set; }
    }
}
