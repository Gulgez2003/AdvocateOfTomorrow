namespace AdvocateOfTomorrow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            List<ContactGetDTO> contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }
        [HttpPut("{id}")]
        // PUT api/<ContactController>/5
        public async Task<IActionResult> UpdateContact(string id, ContactUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }
            await _contactService.UpdateAsync(updateDTO);
            return Ok();
        }
    }
}
