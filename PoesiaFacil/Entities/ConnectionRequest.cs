using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PoesiaFacil.Entities.Contracts;

namespace PoesiaFacil.Entities
{
    public class ConnectionRequest : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserRequesterId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserRequestedId { get; set; }
        public DateTime RequestedDate { get; set; }
        public bool Response {  get; set; }
        public DateTime ResponseDate { get; set; }
    }
}
