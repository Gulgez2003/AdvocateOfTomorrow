namespace Entities.DTOs.ContactDTOs
{
    public class ContactGetDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string ContactInformation { get; set; }
    }
}
