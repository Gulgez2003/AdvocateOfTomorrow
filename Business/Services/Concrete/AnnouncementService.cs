namespace Business.Services.Concrete
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        private readonly IImageService _imageService;
        private readonly FileExtension _fileExtension;

        public AnnouncementService(FileExtension fileExtension, IImageService imageService, IAnnouncementRepository announcementRepository)
        {
            _fileExtension = fileExtension;
            _imageService = imageService;
            _announcementRepository = announcementRepository;
        }

        public async Task CreateAsync(BlogPostDTO postDto)
        {
            Announcement announcement = new Announcement()
            {
                Id = ObjectId.GenerateNewId(),
                Title = postDto.Title,
                Description = postDto.Description,
                AuthorFullName = postDto.AuthorFullName,
                CreatedTime = DateTime.UtcNow
            };

            foreach (var imageDto in postDto.Images)
            {
                var dto = new ImagePostDTO
                {
                    File = imageDto.File,
                    AnnouncementId = announcement.Id
                };

                string remoteImagePath = await _fileExtension.UploadImageAsync("advocateoftomorrow.appspot.com", dto.File, "images");
                await _imageService.CreateAsync(dto);
                announcement.Images.Add(new Image { AnnouncementId = announcement.Id, ImagePath = remoteImagePath });
            }

            await _announcementRepository.CreateAsync(announcement);
        }

        public async Task DeleteAsync(string id)
        {
            Announcement announcement = await _announcementRepository.GetAsync(b => b.Id.ToString() == id && !b.IsDeleted);
            if (announcement == null)
            {
                throw new NotFoundException(Messages.AnnouncementNotFound);
            }

            announcement.IsDeleted = true;

            _announcementRepository.UpdateAsync(announcement);
        }

        public async Task<List<BlogGetDTO>> GetAll()
        {
            List<Announcement> announcements = await _announcementRepository.GetAllAsync(a => !a.IsDeleted);
            if (announcements.Count == 0)
            {
                throw new NotFoundException(Messages.AnnouncementNotFound);
            }

            var announcementsDTOs = announcements.Select(blogGetDTO => new BlogGetDTO
            {
                Id = blogGetDTO.Id,
                Title = blogGetDTO.Title,
                Description = blogGetDTO.Description,
                AuthorFullName = blogGetDTO.AuthorFullName,
                CreatedTime = blogGetDTO.CreatedTime
            }).ToList();

            return announcementsDTOs;
        }

        public async Task<List<BlogGetDTO>> GetAllAsync(int pageNumber)
        {
            int pageSize = 5;
            int skip = (pageNumber - 1) * pageSize;
            Expression<Func<Announcement, object>>[] includes = { s => s.Images };
            List<Announcement> announcements = _announcementRepository.GetAllIncluding(skip, pageSize, includes).ToList();

            var announcementsDTOs = announcements.Select(blogGetDTO => new BlogGetDTO
            {
                Id = blogGetDTO.Id,
                Title = blogGetDTO.Title,
                Description = blogGetDTO.Description,
                AuthorFullName = blogGetDTO.AuthorFullName,
                CreatedTime = blogGetDTO.CreatedTime 
            }).ToList();

            return announcementsDTOs;
        }

        public async Task<BlogGetDTO> GetByIdAsync(string id)
        {
            Expression<Func<Announcement, object>>[] includes = { c => c.Images };
            Announcement announcement = await _announcementRepository.GetIncludingAsync(c => c.Id.ToString() == id, includes);

            if (announcement == null)
            {
                throw new NotFoundException(Messages.AnnouncementNotFound);
            }

            var announcementDTO = new BlogGetDTO
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Description = announcement.Description,
                AuthorFullName = announcement.AuthorFullName,
                CreatedTime = announcement.CreatedTime
            };
            return announcementDTO;
        }

        public async Task UpdateAsync(BlogUpdateDTO updateDto)
        {
            Announcement announcement = await _announcementRepository.GetAsync(b => b.Id.ToString() == updateDto.Id && !b.IsDeleted);

            if (announcement == null)
            {
                throw new NotFoundException(Messages.AnnouncementNotFound);
            }

            announcement.Title = updateDto.BlogPostDTO.Title;
            announcement.Description = updateDto.BlogPostDTO.Description;
            announcement.AuthorFullName = updateDto.BlogPostDTO.AuthorFullName;
            announcement.UpdatedTime = DateTime.UtcNow; 

            _announcementRepository.UpdateAsync(announcement);
        }
    }
}
