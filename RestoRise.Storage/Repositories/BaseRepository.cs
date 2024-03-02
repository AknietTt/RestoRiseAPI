using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Common;

namespace RestoRise.Storage.Repositories;

public abstract class BaseRepository<TEntity> : ICrudRepository<TEntity>
    where TEntity : Entity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly IMapper _mapper;

    protected BaseRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Guid id, string[]? includeProperties = null)
    {
        return await _dbSet.AsNoTracking().ProjectTo<TEntity>(_mapper.ConfigurationProvider, null, includeProperties)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TEntity?> FirstOrDefault(Expression<Func<TEntity, bool>>? filter = null, string[]? includeProperties = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties is { Length: > 0 })
        {
            query = query.ProjectTo<TEntity>(_mapper.ConfigurationProvider, null, includeProperties);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string[]? includeProperties = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includeProperties is { Length: > 0 })
        {
            query = query.ProjectTo<TEntity>(_mapper.ConfigurationProvider, null, includeProperties);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }

        return await query.ToListAsync();
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        return await query.AnyAsync(filter);
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        if (filter != null)
        {
            return await query.CountAsync(filter);
        }

        return await query.CountAsync();
    }

    public async Task DeleteRange(Guid[] ids)
    {
        var entityToDelete = await GetAsync(x => ids.Contains(x.Id));
        DeleteRange(entityToDelete.ToList());
    }

    public async Task AddAsync(TEntity entity)
    {
        var res = await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(TEntity[] entity)
    {
        await _dbSet.AddRangeAsync(entity);
    }

    public virtual async Task Delete(Guid id)
    {
        var entityToDelete = await GetAsync(id);
        if (entityToDelete != null)
            Delete(entityToDelete);
    }

    protected virtual void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }
        _dbSet.Remove(entityToDelete);
    }

    protected virtual void DeleteRange(List<TEntity> entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.AttachRange(entityToDelete);
        }
        _dbSet.RemoveRange(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

}
