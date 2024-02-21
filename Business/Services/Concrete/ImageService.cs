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
            Image image = new Image()
            {
                Id = ObjectId.GenerateNewId(),
                AcademicId = postDto.AcademicId,
                AnnouncementId = postDto.AnnouncementId,
                EventId = postDto.EventId
            };

            string remoteImagePath = await _fileExtension.UploadImageAsync("advocateoftomorrow.appspot.com", postDto.File, "images");

            image.ImagePath = remoteImagePath; 

            await _imageRepository.CreateAsync(image);
        }

        public async Task DeleteAsync(string id)
        {
            Image image = await _imageRepository.GetAsync(a => a.Id.ToString() == id && !a.IsDeleted);
            if (image == null)
            {
                throw new NotFoundException(Messages.ImageNotFound);
            }
            image.IsDeleted = true;

            _imageRepository.UpdateAsync(image);
        }
    }
}
