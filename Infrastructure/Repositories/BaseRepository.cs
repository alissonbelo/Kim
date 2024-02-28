using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext Context;

    public BaseRepository(AppDbContext context)
    {
        Context = context;
    }

    public void Create(T entity)
    {
        Context.Add(entity);
    }

    public void Update(T entity)
    {
        Context.Update(entity);
    }

    public void Delete(T entity)
    {
        Context.Remove(entity);
    }

    public async Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<T>> GetAll(CancellationToken cancellationToken)
    {
        return await Context.Set<T>().ToListAsync(cancellationToken);
    }
}