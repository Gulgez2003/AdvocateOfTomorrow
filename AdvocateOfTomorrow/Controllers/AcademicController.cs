namespace AdvocateOfTomorrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcademicController : ControllerBase
    {
        private readonly IAcademicService _academicService;

        public AcademicController(IAcademicService academicService)
        {
            _academicService = academicService;
        }

        // GET: api/<AcademicController>
        [HttpGet]
        public async Task<IActionResult> GetAllAcademicPerformances(int pageNumber)
        {
            List<BlogGetDTO> academics = await _academicService.GetAllAsync(pageNumber);
            return Ok(academics);
        }

        // GET api/<AcademicController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAcademicPerformanceById(string id)
        {
            BlogGetDTO academicGetDTO = await _academicService.GetByIdAsync(id);
            return Ok(academicGetDTO);
        }

        // POST api/<AcademicController>
        [HttpPost]
        public async Task<IActionResult> PostAcademicPerformance(BlogPostDTO postDTO)
        {
            _academicService.CreateAsync(postDTO);
            return Ok(postDTO);
        }

        // PUT api/<AcademicController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAcademicPerformance(string id, BlogUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }
            await _academicService.UpdateAsync(updateDTO);
            return Ok();
        }

        // DELETE api/<AcademicController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAcademicPerformance(string id)
        {
            await _academicService.DeleteAsync(id);
            return Ok();
        }
    }
}
