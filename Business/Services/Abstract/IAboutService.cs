namespace Business.Services.Abstract
{
    public interface IAboutService
    {
        Task<List<AboutGetDTO>> GetAllAsync();
        Task<AboutGetDTO> GetByIdAsync(string id);
        Task CreateAsync(AboutPostDTO postDto);
        Task UpdateAsync(AboutUpdateDTO updateDto);
        Task DeleteAsync(string id);
    }
}
