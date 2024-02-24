namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Authorize]
    [ApiController]
    [Route("api/blog")]
    public class BlogController : ControllerBase
    {
        private readonly IEntityRepository<Academic> _academicRepository;
        private readonly IEntityRepository<Event> _eventsRepository;
        private readonly IEntityRepository<Announcement> _announcementsRepository;

        public BlogController(
            IEntityRepository<Academic> academicRepository,
            IEntityRepository<Event> eventsRepository,
            IEntityRepository<Announcement> announcementsRepository)
        {
            _academicRepository = academicRepository;
            _eventsRepository = eventsRepository;
            _announcementsRepository = announcementsRepository;
        }

        [HttpGet("getAllBlogs/{pageNumber}")]
        public async Task<IActionResult> GetAllBlogs(int pageNumber = 1)
        {
            try
            {
                var academicBlogs = await _academicRepository.GetAllAsync();
                var eventsBlogs = await _eventsRepository.GetAllAsync();
                var announcementsBlogs = await _announcementsRepository.GetAllAsync();

                var allBlogs = academicBlogs.Cast<Blog>()
                    .Concat(eventsBlogs)
                    .Concat(announcementsBlogs)
                    .OrderByDescending(x => x.CreatedTime)
                    .Skip((pageNumber - 1) * 10)
                    .Take(10)
                    .ToList();

                return Ok(allBlogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
