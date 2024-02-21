using Microsoft.AspNetCore.Http;

namespace Entities.DTOs.ImageDTOs
{
    public class ImagePostDTO
    {
        public IFormFile File { get; set; }
        public ObjectId AcademicId { get; set; }
        public ObjectId AnnouncementId { get; set; }
        public ObjectId EventId { get; set; }
    }
}
