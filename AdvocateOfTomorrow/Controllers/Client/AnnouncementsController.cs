namespace AdvocateOfTomorrow.Controllers.Client
{
    [Route("api/client/announcement")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // GET: api/<AnnouncementsController>
        [HttpGet("getAllAnnouncements/{pageNumber}")]
        public async Task<IActionResult> GetAllAnnouncements(int pageNumber)
        {
            List<BlogGetDTO> announcements = await _announcementService.GetAllAsync(pageNumber);
            return Ok(announcements);
        }
    }
}
