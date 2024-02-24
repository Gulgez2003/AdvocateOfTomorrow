namespace Entities.DTOs.BlogDTOs
{
    public class BlogPostDTO
    {
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        [BsonElement("images")]
        public virtual List<ImagePostDTO> Images { get; set; }
        public BlogPostDTO()
        {
            Images = new List<ImagePostDTO>();
        }
    }
}
