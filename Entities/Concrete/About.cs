namespace Entities.Concrete
{
    public class About : IEntity
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
