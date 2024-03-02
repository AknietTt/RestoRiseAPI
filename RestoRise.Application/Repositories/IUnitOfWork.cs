using RestoRise.Domain.Common;

namespace RestoRise.BuisnessLogic.ICrudRepository;

public interface IUnitOfWork {
    ICrudRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    Task<int> SaveChangesAsync();
}
