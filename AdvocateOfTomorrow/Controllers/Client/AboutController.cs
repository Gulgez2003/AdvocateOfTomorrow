namespace AdvocateOfTomorrow.Controllers.Client
{
    [Route("api/client/about")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        // GET: api/<AboutController>
        [HttpGet("getAbouts")]
        public async Task<IActionResult> GetAbouts()
        {
            List<AboutGetDTO> abouts = await _aboutService.GetAllAsync();
            return Ok(abouts);
        }
    }
}
