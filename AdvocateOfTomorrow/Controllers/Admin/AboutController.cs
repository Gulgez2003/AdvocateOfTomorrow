namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Authorize]
    [Route("api/about")]
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

        // GET api/<AboutController>/5
        [HttpGet("getAboutById/{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            AboutGetDTO aboutGetDTO = await _aboutService.GetByIdAsync(id);
            return Ok(aboutGetDTO);
        }

        // POST api/<AboutController>
        [HttpPost("postAbout")]
        public async Task<IActionResult> PostAbout(AboutPostDTO postDTO)
        {
            AboutPostDTOValidator validations = new AboutPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDTO);
            if (validationResult.IsValid)
            {
                await _aboutService.CreateAsync(postDTO);
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

        // PUT api/<AboutController>/5
        [HttpPut("updateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(string id, AboutUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }

            AboutPostDTOValidator validations = new AboutPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDTO.AboutPostDTO);
            if (validationResult.IsValid)
            {
                await _aboutService.UpdateAsync(updateDTO);
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

        // DELETE api/<AboutController>/5
        [HttpDelete("removeAbout/{id}")]
        public async Task<IActionResult> RemoveAbout(string id)
        {
            await _aboutService.DeleteAsync(id);
            return Ok();
        }
    }
}
