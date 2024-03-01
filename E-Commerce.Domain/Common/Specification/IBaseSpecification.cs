using System.Linq.Expressions;

namespace E_Commerce.Domain.Common.Specification
{
    public interface IBaseSpecification<T>
    {
        Expression<Func<T, bool>> ToExpression();
        bool IsSatisfiedBy(T entity);
    }
}