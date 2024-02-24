namespace AdvocateOfTomorrow.Controllers.Admin
{
    [Authorize]
    [Route("api/contact")]
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
        [HttpPut("updateContact/{id}")]
        // PUT api/<ContactController>/5
        public async Task<IActionResult> UpdateContact(string id, ContactUpdateDTO updateDTO)
        {
            if (id != updateDTO.Id.ToString())
            {
                return BadRequest("Invalid id provided in the request body");
            }

            ContactPostDTOValidator validations = new ContactPostDTOValidator();
            ValidationResult validationResult = await validations.ValidateAsync(updateDTO);
            if (validationResult.IsValid)
            {
                await _contactService.UpdateAsync(updateDTO);
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
    }
}
