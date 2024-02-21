namespace Entities.DTOs.BlogDTOs
{
    public class BlogUpdateDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public BlogPostDTO BlogPostDTO { get; set; }
    }
}
