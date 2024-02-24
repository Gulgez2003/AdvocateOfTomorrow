namespace Core.Entities.DataAccess.Concrete
{
    public class EntityRepositoryBase<TEntity> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        private readonly IMongoCollection<TEntity> _collection;
        private readonly IConfiguration _configuration;

        public EntityRepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
            var client = new MongoClient(_configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "List");
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async void DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null)
        {
            var filter = exp ?? (x => true);
            var cursor = await _collection.Find(filter).ToListAsync();
            return cursor;
        }

        public async Task<TEntity> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var filter = Builders<TEntity>.Filter.Where(predicate);
            var projection = Builders<TEntity>.Projection.Combine(includes.Select(p => Builders<TEntity>.Projection.Include(p)));
            var entity = await _collection.Find(filter).Project<TEntity>(projection).FirstOrDefaultAsync();
            return entity;
        }

        public IEnumerable<TEntity> GetAllIncluding(int pageNumber, int pageSize, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var projection = Builders<TEntity>.Projection.Combine(includeProperties.Select(p => Builders<TEntity>.Projection.Include(p)));
            var cursor = _collection.Find(FilterDefinition<TEntity>.Empty).Project<TEntity>(projection).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToList();
            return cursor;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes)
        {
            var filter = exp ?? (x => true);
            var cursor = await _collection.Find(filter).FirstOrDefaultAsync();
            return cursor;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity);
        }
    }
}
