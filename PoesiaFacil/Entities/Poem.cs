using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PoesiaFacil.Entities
{
    public class Poem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
    }
}
