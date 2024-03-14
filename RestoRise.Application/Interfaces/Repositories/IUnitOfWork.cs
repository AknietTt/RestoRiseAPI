using RestoRise.Domain.Common;
using RestoRise.Domain.Entities;

namespace RestoRise.Application.Interfaces.Repositories;

public interface IUnitOfWork {
    ICrudRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
    Task<int> SaveChangesAsync();
    
}
