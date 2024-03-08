using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.BuisnessLogic.ICrudRepository;

public interface IUnitOfWork {
    ICrudRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    Task<int> SaveChangesAsync();
    
}
