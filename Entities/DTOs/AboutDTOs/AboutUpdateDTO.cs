namespace Entities.DTOs.AboutDTOs
{
    public class AboutUpdateDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public AboutPostDTO AboutPostDTO { get; set; }
    }
}
