namespace Business.Services.Concrete
{
    public class AboutService : IAboutService
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutService(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public async Task CreateAsync(AboutPostDTO postDto)
        {
            About about = new ()
            {
                Id = ObjectId.GenerateNewId(),
                Title = postDto.Title,
                Description = postDto.Description
            };
            await _aboutRepository.CreateAsync(about);
        }

        public async Task DeleteAsync(string id)
        {
            About about = await _aboutRepository.GetAsync(a => a.Id.ToString() == id && !a.IsDeleted);
            if (about == null)
            {
                throw new NotFoundException(Messages.AboutNotFound);
            }
            about.IsDeleted = true;

            await _aboutRepository.UpdateAsync(about);
        }

        public async Task<List<AboutGetDTO>> GetAllAsync()
        {
            List<About> abouts = await _aboutRepository.GetAllAsync(a => !a.IsDeleted);
            if (abouts.Count == 0)
            {
                throw new NotFoundException(Messages.AboutNotFound);
            }

            var aboutDTOs = abouts.Select(aboutGetDTO => new AboutGetDTO
            {
                Id = aboutGetDTO.Id,
                Title = aboutGetDTO.Title,
                Description = aboutGetDTO.Description
            }).ToList(); 

            return aboutDTOs;
        }

        public async Task<AboutGetDTO> GetByIdAsync(string id)
        {
            About about = await _aboutRepository.GetAsync(a => a.Id.ToString() == id && !a.IsDeleted);
            if (about == null)
            {
                throw new NotFoundException(Messages.AboutNotFound);
            }
            var aboutDTO = new AboutGetDTO
            {
                Id = about.Id,
                Title = about.Title,
                Description = about.Description
            };
            return aboutDTO;
        }

        public async Task UpdateAsync(AboutUpdateDTO updateDto)
        {
            About about = await _aboutRepository.GetAsync(a => a.Id.ToString() == updateDto.Id && !a.IsDeleted);
            if (about == null)
            {
                throw new NotFoundException(Messages.AboutNotFound);
            }
            about.Title = updateDto.AboutPostDTO.Title;
            about.Description = updateDto.AboutPostDTO.Description;

            await _aboutRepository.UpdateAsync(about);
        }
    }
}
