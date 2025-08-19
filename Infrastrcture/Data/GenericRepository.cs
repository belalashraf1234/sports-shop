using Core.Entity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastrcture.Data;

public class GenericRepository<T>(StoreContext context):IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
         
    }

    public void Delete(T entity)
    {
      context.Set<T>().Remove(entity);
    }

    public async Task<T?> GetByIdAsync(int id)
    {
       return await context.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T?>> ListAllAsync()
    {
      return await context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T?>> ListAsync(ISpcification<T> spec)
    {   
       return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T?> GetWithSpec(ISpcification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<TResult?>> ListAsync<TResult>(ISpcification<T, TResult> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<TResult?> GetWithSpec<TResult>(ISpcification<T, TResult> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public bool EntityExists(int id)
    {
        return context.Set<T>().Any(x=>x.Id == id);
    }

    public async Task<int> CountAsync(ISpcification<T> spec)
    {
        var query=context.Set<T>().AsQueryable();
        query = spec.ApplyCriteria(query);
        return await query.CountAsync();
    }

    private IQueryable<T> ApplySpecification(ISpcification<T> spec)
    {
        return SpecificationEvaluatour<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
       
    }
   
    private IQueryable<TResult> ApplySpecification<TResult>(ISpcification<T,TResult> spec)
    {
        return SpecificationEvaluatour<T>.GetQuery<T,TResult>(context.Set<T>().AsQueryable(), spec);
    }
}