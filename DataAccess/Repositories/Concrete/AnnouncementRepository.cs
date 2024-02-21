

namespace DataAccess.Repositories.Concrete
{
    public class AnnouncementRepository : EntityRepositoryBase<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
