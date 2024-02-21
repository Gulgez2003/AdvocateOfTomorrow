namespace AdvocateOfTomorrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        // GET: api/<AboutController>
        [HttpGet]
        public async Task<IActionResult> GetAbouts()
        {
            List<AboutGetDTO> abouts = await _aboutService.GetAllAsync();
            return Ok(abouts);
        }

        // GET api/<AboutController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            AboutGetDTO aboutGetDTO = await _aboutService.GetByIdAsync(id);
            return Ok(aboutGetDTO);
        }

        // POST api/<AboutController>
        [HttpPost]
        public async Task<IActionResult> PostAbout(AboutPostDTO postDTO)
        {
            _aboutService.CreateAsync(postDTO);
            return Ok(postDTO);
        }

        // PUT api/<AboutController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbout(string id, AboutUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }
            await _aboutService.UpdateAsync(updateDTO);
            return Ok();
        }

        // DELETE api/<AboutController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAbout(string id)
        {          
            await _aboutService.DeleteAsync(id);
            return Ok();
        }
    }
}
