using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using PoesiaFacil.Entities.Contracts;
using PoesiaFacil.Validators;
using FluentValidation.Results;

namespace PoesiaFacil.Entities
{
    public class Poem : IEntity
    {
        public Poem()
        {
                
        }

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

        public bool IsValid()
        {
            PoemValidator validator = new PoemValidator();

            ValidationResult result = validator.Validate(this);

            return result.IsValid;
        }
    }
}
