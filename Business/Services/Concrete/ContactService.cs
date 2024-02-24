namespace Business.Services.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<ContactGetDTO>> GetAllAsync()
        {
            List<Contact> contacts = await _contactRepository.GetAllAsync();
            if (contacts.Count == 0)
            {
                throw new NotFoundException(Messages.ContactNotFound);
            }
            var contactDTOs = contacts.Select(contactGetDTO => new ContactGetDTO
            {
                Id = contactGetDTO.Id,
                Title = contactGetDTO.Title,
                ContactInformation = contactGetDTO.ContactInformation
            }).ToList();

            return contactDTOs;
        }

        public async Task UpdateAsync(ContactUpdateDTO updateDto)
        {
            Contact contact = await _contactRepository.GetAsync(s => s.Id.ToString() == updateDto.Id);
            if (contact == null)
            {
                throw new NotFoundException(Messages.ContactNotFound);
            }

            contact.Title = updateDto.Title;
            contact.ContactInformation = updateDto.ContactInformation;

            await _contactRepository.UpdateAsync(contact);
        }
    }
}
