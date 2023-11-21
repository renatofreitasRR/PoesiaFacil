using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Identity;

namespace PoesiaFacil.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string GoogleId { get; set; }
        public string Name { get; set; }
        public string ArtisticName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ShowOnlyArtisticName { get; set; }
        public bool IsActive { get; set; }
        public IList<Poem>? Poems { get; set; }
        public IList<string>? Connections { get; set; }

        public void Activate()
        {
            this.IsActive = true;
        }

        public void Deactivate()
        {
            this.IsActive = false;
        }
    }
}
