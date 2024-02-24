namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Authorize]
    [Route("api/announcement")]
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

        // GET api/<AnnouncementsController>/5
        [HttpGet("getAnnouncementById/{id}")]
        public async Task<IActionResult> GetAnnouncementById(string id)
        {
            BlogGetDTO announcementGetDTO = await _announcementService.GetByIdAsync(id);
            return Ok(announcementGetDTO);
        }

        // POST api/<AnnouncementsController>
        [HttpPost("postAnnouncement")]
        public async Task<IActionResult> PostAnnouncement(BlogPostDTO postDTO)
        {
            BlogPostDTOValidator validations = new BlogPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDTO);
            if (validationResult.IsValid)
            {
                _announcementService.CreateAsync(postDTO);
                return Ok(postDTO);
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        // PUT api/<AnnouncementsController>/5
        [HttpPut("updateAnnouncement/{id}")]
        public async Task<IActionResult> UpdateAnnouncement(string id, BlogUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }

            BlogPostDTOValidator validations = new BlogPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDTO.BlogPostDTO);
            if (validationResult.IsValid)
            {
                await _announcementService.UpdateAsync(updateDTO);
                return Ok();
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return BadRequest(ModelState);
            }
        }

        // DELETE api/<AnnouncementsController>/5
        [HttpDelete("removeAnnouncement/{id}")]
        public async Task<IActionResult> RemoveAnnouncement(string id)
        {
            await _announcementService.DeleteAsync(id);
            return Ok();
        }
    }
}
