namespace Business.Services.Abstract
{
    public interface IImageService
    {
        Task CreateAsync(ImagePostDTO postDto);
        Task DeleteAsync(string id);
    }
}
