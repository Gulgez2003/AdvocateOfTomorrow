namespace Entities.DTOs.AboutDTOs
{
    public class AboutGetDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsDeleted { get; set; }
    }
}
