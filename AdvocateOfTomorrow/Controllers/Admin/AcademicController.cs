namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Authorize]
    [Route("api/academic")]
    [ApiController]
    public class AcademicController : ControllerBase
    {
        private readonly IAcademicService _academicService;

        public AcademicController(IAcademicService academicService)
        {
            _academicService = academicService;
        }

        // GET: api/<AcademicController>
        [HttpGet("getAllAcademicPerformances/{pageNumber}")]
        public async Task<IActionResult> GetAllAcademicPerformances(int pageNumber)
        {
            List<BlogGetDTO> academics = await _academicService.GetAllAsync(pageNumber);
            return Ok(academics);
        }

        // GET api/<AcademicController>/5
        [HttpGet("getAcademicPerformanceById/{id}")]
        public async Task<IActionResult> GetAcademicPerformanceById(string id)
        {
            BlogGetDTO academicGetDTO = await _academicService.GetByIdAsync(id);
            return Ok(academicGetDTO);
        }

        // POST api/<AcademicController>
        [HttpPost("postAcademicPerformance")]
        public async Task<IActionResult> PostAcademicPerformance(BlogPostDTO postDTO)
        {
            BlogPostDTOValidator validations = new BlogPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(postDTO);
            if (validationResult.IsValid)
            {
                _academicService.CreateAsync(postDTO);
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

        // PUT api/<AcademicController>/5
        [HttpPut("updateAcademicPerformance/{id}")]
        public async Task<IActionResult> UpdateAcademicPerformance(string id, BlogUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }

            BlogPostDTOValidator validations = new BlogPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDTO.BlogPostDTO);
            if (validationResult.IsValid)
            {
                await _academicService.UpdateAsync(updateDTO);
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

        // DELETE api/<AcademicController>/5
        [HttpDelete("removeAcademicPerformance/{id}")]
        public async Task<IActionResult> RemoveAcademicPerformance(string id)
        {
            await _academicService.DeleteAsync(id);
            return Ok();
        }
    }
}
