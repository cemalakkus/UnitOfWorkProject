using Microsoft.EntityFrameworkCore;
using UnitOfWorkPattern.Application.Interfaces.Repositories;
using UnitOfWorkPattern.Domain.Common;
using UnitOfWorkPattern.Persistence.Context;

namespace UnitOfWorkPattern.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _dbContext;
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().AsNoTracking()
                                        .ToListAsync();
    }

    public async Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return await _dbContext.Set<T>().Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .AsNoTracking()
                                        .ToListAsync();
    }
    
    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
}
