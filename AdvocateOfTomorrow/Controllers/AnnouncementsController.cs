namespace AdvocateOfTomorrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        // GET: api/<AnnouncementsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements(int pageNumber)
        {
            List<BlogGetDTO> announcements = await _announcementService.GetAllAsync(pageNumber);
            return Ok(announcements);
        }

        // GET api/<AnnouncementsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(string id)
        {
            BlogGetDTO announcementGetDTO = await _announcementService.GetByIdAsync(id);
            return Ok(announcementGetDTO);
        }

        // POST api/<AnnouncementsController>
        [HttpPost]
        public async Task<IActionResult> PostAnnouncement(BlogPostDTO postDTO)
        {
            _announcementService.CreateAsync(postDTO);
            return Ok(postDTO);
        }

        // PUT api/<AnnouncementsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(string id, BlogUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }
            await _announcementService.UpdateAsync(updateDTO);
            return Ok();
        }

        // DELETE api/<AnnouncementsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAnnouncement(string id)
        {
            await _announcementService.DeleteAsync(id);
            return Ok();
        }
    }
}
