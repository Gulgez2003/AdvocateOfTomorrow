namespace Business.Services.Abstract
{
    public interface IContactService
    {
        Task<List<ContactGetDTO>> GetAllAsync();
        Task UpdateAsync(ContactUpdateDTO updateDto);
    }
}
