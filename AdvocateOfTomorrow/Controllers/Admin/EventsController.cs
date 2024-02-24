namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Authorize]
    [Route("api/event")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/<EventsController>
        [HttpGet("getAllEvents/{pageNumber}")]
        public async Task<IActionResult> GetAllEvents(int pageNumber)
        {
            List<BlogGetDTO> events = await _eventService.GetAllAsync(pageNumber);
            return Ok(events);
        }

        // GET api/<EventsController>/5
        [HttpGet("getEventById/{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            BlogGetDTO eventGetDTO = await _eventService.GetByIdAsync(id);
            return Ok(eventGetDTO);
        }

        // POST api/<EventsController>
        [HttpPost("postEvent")]
        public async Task<IActionResult> PostEvent(BlogPostDTO postDTO)
        {
            BlogPostDTOValidator validations = new BlogPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDTO);
            if (validationResult.IsValid)
            {
                _eventService.CreateAsync(postDTO);
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

        // PUT api/<EventsController>/5
        [HttpPut("updateEvent/{id}")]
        public async Task<IActionResult> UpdateEvent(string id, BlogUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }

            BlogPostDTOValidator validations = new BlogPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDTO.BlogPostDTO);
            if (validationResult.IsValid)
            {
                await _eventService.UpdateAsync(updateDTO);
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

        // DELETE api/<EventsController>/5
        [HttpDelete("removeEvent/{id}")]
        public async Task<IActionResult> RemoveEvent(string id)
        {
            await _eventService.DeleteAsync(id);
            return Ok();
        }
    }
}
