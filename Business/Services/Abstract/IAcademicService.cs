namespace Business.Services.Abstract
{
    public interface IAcademicService
    {
        Task<List<BlogGetDTO>> GetAllAsync(int pageNumber);
        Task<List<BlogGetDTO>> GetAll();
        Task<BlogGetDTO> GetByIdAsync(string id);
        Task CreateAsync(BlogPostDTO postDto);
        Task UpdateAsync(BlogUpdateDTO updateDto);
        Task DeleteAsync(string id);
    }
}
