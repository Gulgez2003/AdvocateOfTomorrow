

namespace DataAccess.Repositories.Concrete
{
    public class ContactRepository : EntityRepositoryBase<Contact>, IContactRepository
    {
        public ContactRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
