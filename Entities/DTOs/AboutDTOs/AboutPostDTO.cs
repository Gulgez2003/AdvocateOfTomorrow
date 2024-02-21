namespace Entities.DTOs.AboutDTOs
{
    public class AboutPostDTO
    {
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
    }
}
