
namespace DataAccess.Repositories.Concrete
{
    public class ImageRepository : EntityRepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
