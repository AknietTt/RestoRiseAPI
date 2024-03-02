using Microsoft.Extensions.DependencyInjection;
using RestoRise.BuisnessLogic.ICrudRepository;
using RestoRise.Domain.Common;

namespace RestoRise.Storage;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceScope _serviceScope;
    private readonly AppDbContext _context;

    public UnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceScope = serviceProvider.CreateScope();
        _context = _serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    }
    
    public ICrudRepository<TDomain> GetRepository<TDomain>()
        where TDomain : Entity
    {
        return (_serviceScope.ServiceProvider.GetRequiredService(typeof(ICrudRepository<TDomain>)) as ICrudRepository<TDomain>)!;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }


}