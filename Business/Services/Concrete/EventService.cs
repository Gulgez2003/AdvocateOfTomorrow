namespace Business.Services.Concrete
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IImageService _imageService;
        private readonly FileExtension _fileExtension;
        public EventService(IEventRepository eventRepository, IImageService imageService, FileExtension fileExtension)
        {
            _eventRepository = eventRepository;
            _imageService = imageService;
            _fileExtension = fileExtension;
        }
        public async Task CreateAsync(BlogPostDTO postDto)
        {
            Event _event = new Event()
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
                    File = imageDto.File
                };

                List<string> remoteImagePaths = await _fileExtension.UploadImagesAsync("advocateoftomorrow.appspot.com", new List<IFormFile> { dto.File }, "images");
                _event.Images.AddRange(remoteImagePaths.Select(path => new Image { EventId = _event.Id, ImagePath = path }));
            }

            await _eventRepository.CreateAsync(_event);
        }

        public async Task DeleteAsync(string id)
        {
            Event _event = await _eventRepository.GetAsync(b => b.Id.ToString() == id && !b.IsDeleted);
            if (_event == null)
            {
                throw new NotFoundException(Messages.EventNotFound);
            }

            _event.IsDeleted = true;

            _eventRepository.UpdateAsync(_event);
        }

        public async Task<List<BlogGetDTO>> GetAll()
        {
            List<Event> events = await _eventRepository.GetAllAsync(a => !a.IsDeleted);
            if (events.Count == 0)
            {
                throw new NotFoundException(Messages.EventNotFound);
            }

            var eventDTOs = events.Select(blogGetDTO => new BlogGetDTO
            {
                Id = blogGetDTO.Id,
                Title = blogGetDTO.Title,
                Description = blogGetDTO.Description,
                AuthorFullName = blogGetDTO.AuthorFullName,
                CreatedTime = blogGetDTO.CreatedTime
            }).ToList();

            return eventDTOs;
        }

        public async Task<List<BlogGetDTO>> GetAllAsync(int pageNumber)
        {
            int pageSize = 5;
            int skip = (pageNumber - 1) * pageSize;
            Expression<Func<Event, object>>[] includes = { s => s.Images };
            List<Event> events = _eventRepository.GetAllIncluding(skip, pageSize, includes).ToList();

            var eventDTOs = events.Select(blogGetDTO => new BlogGetDTO
            {
                Id = blogGetDTO.Id,
                Title = blogGetDTO.Title,
                Description = blogGetDTO.Description,
                AuthorFullName = blogGetDTO.AuthorFullName,
                CreatedTime = blogGetDTO.CreatedTime
            }).ToList();

            return eventDTOs;
        }

        public async Task<BlogGetDTO> GetByIdAsync(string id)
        {
            Expression<Func<Event, object>>[] includes = { c => c.Images };
            Event _event = await _eventRepository.GetIncludingAsync(c => c.Id.ToString() == id, includes);

            if (_event == null)
            {
                throw new NotFoundException(Messages.EventNotFound);
            }

            var eventDTO = new BlogGetDTO
            {
                Id = _event.Id,
                Title = _event.Title,
                Description = _event.Description,
                AuthorFullName = _event.AuthorFullName,
                CreatedTime = _event.CreatedTime
            };
            return eventDTO;
        }

        public async Task UpdateAsync(BlogUpdateDTO updateDto)
        {
            Event _event = await _eventRepository.GetAsync(b => b.Id.ToString() == updateDto.Id && !b.IsDeleted);

            if (_event == null)
            {
                throw new NotFoundException(Messages.EventNotFound);
            }

            _event.Title = updateDto.BlogPostDTO.Title;
            _event.Description = updateDto.BlogPostDTO.Description;
            _event.AuthorFullName = updateDto.BlogPostDTO.AuthorFullName;
            _event.UpdatedTime = DateTime.UtcNow;

            foreach (var imageDto in updateDto.BlogPostDTO.Images)
            {
                var dto = new ImagePostDTO
                {
                    File = imageDto.File
                };

                List<string> remoteImagePaths = await _fileExtension.UploadImagesAsync("advocateoftomorrow.appspot.com", new List<IFormFile> { dto.File }, "images");

                _event.Images.AddRange(remoteImagePaths.Select(path => new Image { EventId = _event.Id, ImagePath = path }));
            }
            _eventRepository.UpdateAsync(_event);
        }
    }
}
