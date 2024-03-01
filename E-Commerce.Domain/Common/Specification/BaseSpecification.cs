using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Specification
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public readonly Func<T, bool> _expression;

        public BaseSpecification(Func<T, bool> expression)
        {
            _expression = expression;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return _expression(entity);
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            return x => _expression(x);
        }
    }
}
