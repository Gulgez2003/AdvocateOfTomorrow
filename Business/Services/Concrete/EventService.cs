namespace Business.Services.Concrete
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;
        public EventService(IEventRepository eventRepository, IImageService imageService, IUserService userService)
        {
            _eventRepository = eventRepository;
            _imageService = imageService;
            _userService = userService;
        }
        public async Task CreateAsync(BlogPostDTO postDto)
        {
            string authorFullName = await _userService.GetCurrentUserName();

            Event _event = new ()
            {
                Id = ObjectId.GenerateNewId(),
                Title = postDto.Title,
                Description = postDto.Description,
                AuthorFullName = authorFullName,
                CreatedTime = DateTime.UtcNow,
                Images= new List<Image>()
            };

            foreach (var imageDto in postDto.Images)
            {
                var dto = new ImagePostDTO
                {
                    File = imageDto.File
                };

                string imagePath = await _imageService.CreateAsync(dto);

                Image image = new()
                {
                    ImagePath = imagePath,
                    EventId = _event.Id
                };

                _event.Images.Add(image);
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

            await _eventRepository.UpdateAsync(_event);
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
            _event.UpdatedTime = DateTime.UtcNow;
            _event.Images = new List<Image>();

            foreach (var imageDto in updateDto.BlogPostDTO.Images)
            {
                var dto = new ImagePostDTO
                {
                    File = imageDto.File
                };

                string imagePath = await _imageService.CreateAsync(dto);

                Image image = new()
                {
                    ImagePath = imagePath,
                    EventId = _event.Id
                };

                _event.Images.Add(image);
            }

            await _eventRepository.UpdateAsync(_event);
        }
    }
}
