namespace Business.Services.Abstract
{
    public interface IImageService
    {
        Task<string> CreateAsync(ImagePostDTO postDto);
        Task DeleteAsync(string id);
    }
}
