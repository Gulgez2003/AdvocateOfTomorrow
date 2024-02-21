namespace Entities.Concrete
{
    public class Image : IEntity
    {
        public ObjectId Id { get; set; }
        public string ImagePath { get; set; }
        public Academic Academic { get; set; }
        public Announcement Announcement { get; set; }
        public Event Event { get; set; }
        public ObjectId AcademicId { get; set; }
        public ObjectId AnnouncementId { get; set; }
        public ObjectId EventId { get; set; }
        public bool IsDeleted {  get; set; }
    }
}
