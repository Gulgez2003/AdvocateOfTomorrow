namespace AdvocateOfTomorrow.Controllers.Client
{
    [Route("api/client/contact")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactController>
        [HttpGet("getContact")]
        public async Task<IActionResult> GetAllContacts()
        {
            List<ContactGetDTO> contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }
    }
}
