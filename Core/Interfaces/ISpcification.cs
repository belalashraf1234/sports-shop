using System.Linq.Expressions;
using Core.Entity;

namespace Core.Interfaces;

public interface ISpcification<T> 
{
    Expression<Func<T,bool>>? Criteria { get;  }
    Expression<Func<T,object>>? OrderBy { get;  }
    Expression<Func<T,object>>? OrderByDescending { get;  }
    bool isDistinct { get;  }
    int Take { get; }
    int Skip { get; }
    bool isPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}
public interface ISpcification<T,TResult>:ISpcification<T>
{
    Expression<Func<T,TResult>>? select { get;  }
}