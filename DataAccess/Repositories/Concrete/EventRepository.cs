

namespace DataAccess.Repositories.Concrete
{
    public class EventRepository : EntityRepositoryBase<Event>, IEventRepository
    {
        public EventRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
