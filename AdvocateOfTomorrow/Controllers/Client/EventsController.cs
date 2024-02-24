namespace AdvocateOfTomorrow.Controllers.Client
{
    [Route("api/client/event")]
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
    }
}
