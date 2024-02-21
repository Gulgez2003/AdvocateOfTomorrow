namespace Business.Services.Concrete
{
    public class AcademicService : IAcademicService
    {
        private readonly IAcademicRepository _academicRepository;
        private readonly IImageService _imageService;
        private readonly FileExtension _fileExtension;
        public AcademicService(IAcademicRepository academicRepository, IImageService imageService, FileExtension fileExtension)
        {
            _academicRepository = academicRepository;
            _imageService = imageService;
            _fileExtension = fileExtension;
        }

        public async Task CreateAsync(BlogPostDTO postDto)
        {
            Academic academic = new Academic()
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
                    AcademicId = academic.Id
                };

                string remoteImagePath = await _fileExtension.UploadImageAsync("advocateoftomorrow.appspot.com", dto.File, "images");
                await _imageService.CreateAsync(dto);
                academic.Images.Add(new Image { AcademicId = academic.Id, ImagePath=remoteImagePath});
            }
            await _academicRepository.CreateAsync(academic);
        }

        public async Task DeleteAsync(string id)
        {
            Academic academic = await _academicRepository.GetAsync(b => b.Id.ToString() == id && !b.IsDeleted);
            if (academic == null)
            {
                throw new NotFoundException(Messages.AcademicNotFound);
            }

            academic.IsDeleted = true;

            _academicRepository.UpdateAsync(academic);
        }

        public async Task<List<BlogGetDTO>> GetAll()
        {
            List<Academic> academics = await _academicRepository.GetAllAsync(a => !a.IsDeleted);
            if (academics.Count == 0)
            {
                throw new NotFoundException(Messages.AcademicNotFound);
            }

            var academicDTOs = academics.Select(blogGetDTO => new BlogGetDTO
            {
                Id = blogGetDTO.Id,
                Title = blogGetDTO.Title,
                Description = blogGetDTO.Description,
                AuthorFullName = blogGetDTO.AuthorFullName,
                CreatedTime = blogGetDTO.CreatedTime
            }).ToList();

            return academicDTOs;
        }

        public async Task<List<BlogGetDTO>> GetAllAsync(int pageNumber)
        {
            int pageSize = 5;
            int skip = (pageNumber - 1) * pageSize;
            Expression<Func<Academic, object>>[] includes = { s => s.Images };
            List<Academic> academics = _academicRepository.GetAllIncluding(skip, pageSize, includes).ToList();

            var academicDTOs = academics.Select(blogGetDTO => new BlogGetDTO
            {
                Id = blogGetDTO.Id,
                Title = blogGetDTO.Title,
                Description = blogGetDTO.Description,
                AuthorFullName = blogGetDTO.AuthorFullName,
                CreatedTime = blogGetDTO.CreatedTime
            }).ToList();

            return academicDTOs;
        }

        public async Task<BlogGetDTO> GetByIdAsync(string id)
        {
            Expression<Func<Academic, object>>[] includes = { c => c.Images };
            Academic academic = await _academicRepository.GetIncludingAsync(c => c.Id.ToString() == id, includes);

            if (academic == null)
            {
                throw new NotFoundException(Messages.AcademicNotFound);
            }

            var academicDTO = new BlogGetDTO
            {
                Id = academic.Id,
                Title = academic.Title,
                Description = academic.Description,
                AuthorFullName = academic.AuthorFullName,
                CreatedTime = academic.CreatedTime
            };
            return academicDTO;
        }

        public async Task UpdateAsync(BlogUpdateDTO updateDto)
        {
            Academic academic = await _academicRepository.GetAsync(b => b.Id.ToString() == updateDto.Id && !b.IsDeleted);

            if (academic == null)
            {
                throw new NotFoundException(Messages.AcademicNotFound);
            }

            academic.Title = updateDto.BlogPostDTO.Title;
            academic.Description = updateDto.BlogPostDTO.Description;
            academic.AuthorFullName = updateDto.BlogPostDTO.AuthorFullName;
            academic.UpdatedTime = DateTime.UtcNow;

            _academicRepository.UpdateAsync(academic);
        }
    }
}
