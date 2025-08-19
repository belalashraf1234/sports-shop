using Core.Entity;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T?>> ListAllAsync();
    Task<IReadOnlyList<T?>> ListAsync(ISpcification<T> spec);
    Task<T?> GetWithSpec(ISpcification<T> spec);
    Task<IReadOnlyList<TResult?>> ListAsync<TResult>(ISpcification<T,TResult> spec);
    Task<TResult?> GetWithSpec<TResult>(ISpcification<T,TResult> spec);
    public Task<bool> SaveChangesAsync();
    
    public bool EntityExists(int id);
    Task<int> CountAsync(ISpcification<T> spec);

}