namespace Core.Entities.DataAccess.Abstract
{
    public interface IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> exp = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp, params string[] includes);
        Task<TEntity> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAllIncluding(int pageNumber, int take, Expression<Func<TEntity, object>>[] includeProperties);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void DeleteAsync(string id);
    }
}
