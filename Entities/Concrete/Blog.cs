using Entities.Concrete.Enums;

namespace Entities.Concrete
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Title { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string Description { get; set; }
        [BsonRepresentation(BsonType.String)]
        public string AuthorFullName { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedTime { get; set; }
        [BsonElement("images")]
        public virtual List<Image> Images { get; set; }
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsDeleted { get; set; }
        [BsonElement("blogType")]
        public BlogTypes BlogType { get; set; }
        public Blog()
        {
            Images = new List<Image>();
        }
    }
}
