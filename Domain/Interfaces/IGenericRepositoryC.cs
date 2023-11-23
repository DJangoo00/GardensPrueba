using System.Linq.Expressions;
using Domain.Entities;
public interface IGenericRepositoryC<T> where T : BaseEntityC
{
    Task<T> GetByIdAsync(int idi, string idS);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}