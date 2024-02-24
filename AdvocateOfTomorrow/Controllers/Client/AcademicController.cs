namespace AdvocateOfTomorrow.Controllers.Client
{
    [Route("api/client/academic")]
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
    }
}
