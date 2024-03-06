using System.Linq.Expressions;
using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.ICrudRepository;

public interface ICrudRepository<TEntity>
    where TEntity : Entity
{
   // Task<TEntity?> GetAsync(Guid id, string[]? includeProperties = null);
    Task<TEntity?> GetAsync(Guid id, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, string[]? includeProperties = null);
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? includeProperties = null);
    Task<bool> Any(Expression<Func<TEntity, bool>> filter);
    Task<int> Count(Expression<Func<TEntity, bool>>? filter = null);
    Task Delete(Guid id);
    Task DeleteRange(Guid[] ids);
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(TEntity[] entity);
    void Update(TEntity entityToUpdate);
}
