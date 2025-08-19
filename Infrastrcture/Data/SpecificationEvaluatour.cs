using Core.Entity;
using Core.Interfaces;

namespace Infrastrcture.Data;

public class SpecificationEvaluatour<T> where  T:BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpcification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.isDistinct)
        {
            query=query.Distinct();
        }

        if (spec.isPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;
    }
    public static IQueryable<TResult> GetQuery<T, TResult>(IQueryable<T> query, 
        ISpcification<T,TResult> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }
        if(spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }
    
        var selectQuery = query as IQueryable<TResult>;
    
        if(spec.select != null)
        {
            selectQuery = query.Select(spec.select);
        }
        if (spec.isDistinct)
        { 
            selectQuery=selectQuery?.Distinct();
        }
        if (spec.isPagingEnabled)
        {
            selectQuery = selectQuery.Skip(spec.Skip).Take(spec.Take);
        }
        return selectQuery??query.Cast<TResult>();
    }
}