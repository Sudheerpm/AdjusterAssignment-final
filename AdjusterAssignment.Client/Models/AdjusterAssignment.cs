using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AdjusterAssignment.Client.Models
{
    public class AdjAssignment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AdjuterId { get; set; }
        public string AdjusterName { get; set; }
        public string ClaimId { get; set; }
        public string ClaimGroupId { get; set; }
        
    }
}
