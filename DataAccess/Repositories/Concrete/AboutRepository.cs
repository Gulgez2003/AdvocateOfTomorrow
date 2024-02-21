
namespace DataAccess.Repositories.Concrete
{
    public class AboutRepository : EntityRepositoryBase<About>, IAboutRepository
    {
        public AboutRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
