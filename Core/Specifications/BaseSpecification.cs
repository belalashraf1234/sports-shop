using System.Linq.Expressions;
using Core.Entity;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria):ISpcification<T> where T : BaseEntity
{
    public BaseSpecification() : this(null)
    {
        
    }
    public Expression<Func<T, bool>>? Criteria => criteria;
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public bool isDistinct { get; private set; }
    public int Take { get; private set; }
    public int Skip { get;private set; }
    public bool isPagingEnabled { get;private set; }
    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (criteria != null)
        {
            query = query.Where(criteria).Skip(Skip).Take(Take);
            
        }

        return query;
    }

    protected void AddOrderBy(Expression<Func<T, object>> expression)
    {
        OrderBy = expression;
    }
    protected void AddOrderByDesc(Expression<Func<T, object>> expression)
    {
        OrderByDescending = expression;
    }

    protected void AddDistinct()
    {
        isDistinct = true;
        
    }
    protected void ApplyPagination(int skip, int take)
    {
        Skip=skip;
        Take=take;
        isPagingEnabled=true;
    }
}  
public class BaseSpecification<T,TResult>(Expression<Func<T, bool>>? criteria):BaseSpecification<T>(criteria),ISpcification<T,TResult> where T : BaseEntity
{   public BaseSpecification():this(null){}
    public Expression<Func<T, TResult>>? select { get; private set; }
    protected void AddSelect(Expression<Func<T, TResult>> expression)
    {
        select = expression;
    }
    
}