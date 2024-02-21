

namespace DataAccess.Repositories.Concrete
{
    public class AcademicRepository : EntityRepositoryBase<Academic>, IAcademicRepository
    {
        public AcademicRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
