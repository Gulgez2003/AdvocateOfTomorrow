using Microsoft.AspNetCore.Http;

namespace Business.Services.Concrete
{
    public class ImageService : IImageService
    {
        private readonly FileExtension _fileExtension;
        private readonly IImageRepository _imageRepository;

        public ImageService(FileExtension fileExtension, IImageRepository imageRepository)
        {
            _fileExtension = fileExtension;
            _imageRepository = imageRepository;
        }

        public async Task CreateAsync(ImagePostDTO postDto)
        {
            if (!_fileExtension.IsImage(postDto.File))
            {
                throw new BadRequestException("Uploaded file is not an image.");
            }

            if (!_fileExtension.IsSizeOk(postDto.File, 10)) 
            {
                throw new BadRequestException("File size exceeds the limit.");
            }

            List<string> remoteImagePaths = await _fileExtension.UploadImagesAsync("advocateoftomorrow.appspot.com", new List<IFormFile> { postDto.File }, "images");

            await Task.WhenAll(remoteImagePaths.Select(async remoteImagePath =>
            {
                Image image = new Image()
                {
                    Id = ObjectId.GenerateNewId(),
                    ImagePath = remoteImagePath
                };

                await _imageRepository.CreateAsync(image);
            }));
        }

        public async Task DeleteAsync(string id)
        {
            Image image = await _imageRepository.GetAsync(a => a.Id.ToString() == id && !a.IsDeleted);
            if (image == null)
            {
                throw new NotFoundException(Messages.ImageNotFound);
            }

            image.IsDeleted = true;

            await _imageRepository.UpdateAsync(image);
        }
    }
}
