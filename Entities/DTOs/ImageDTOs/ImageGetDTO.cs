namespace Entities.DTOs.ImageDTOs
{
    public class ImageGetDTO
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public Academic Academic { get; set; }
        public Announcement Announcement { get; set; }
        public Event Event { get; set; }
    }
}
