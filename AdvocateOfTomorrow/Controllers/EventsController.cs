namespace AdvocateOfTomorrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/<EventsController>
        [HttpGet]
        public async Task<IActionResult> GetAllEvents(int pageNumber)
        {
            List<BlogGetDTO> events = await _eventService.GetAllAsync(pageNumber);
            return Ok(events);
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            BlogGetDTO eventGetDTO = await _eventService.GetByIdAsync(id);
            return Ok(eventGetDTO);
        }

        // POST api/<EventsController>
        [HttpPost]
        public async Task<IActionResult> PostEvent(BlogPostDTO postDTO)
        {
            _eventService.CreateAsync(postDTO);
            return Ok(postDTO);
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, BlogUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }
            await _eventService.UpdateAsync(updateDTO);
            return Ok();
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveEvent(string id)
        {
            await _eventService.DeleteAsync(id);
            return Ok();
        }
    }
}
