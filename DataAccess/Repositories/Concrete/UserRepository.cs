namespace DataAccess.Repositories.Concrete
{
    public class UserRepository : EntityRepositoryBase<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
