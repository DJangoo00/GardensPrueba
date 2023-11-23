using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
//namespaces internos
using Domain.Entities;
using Domain.Interfaces;
using Persistence;
namespace App.Repository;
public class GenericRepositoryC<T> : IGenericRepositoryC<T> where T : BaseEntityC
{
    private readonly ApiContext _context;

    public GenericRepositoryC(ApiContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int idI, string idS)
    {
        return await _context.Set<T>().FindAsync(idI, idS);
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>()
            .Update(entity);
    }
}