using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Common.Specification
{
    public class AndSpecification<T> : IBaseSpecification<T>
    {
        public readonly IBaseSpecification<T> rightSpecification;
        public readonly IBaseSpecification<T> leftSpecification;

        public AndSpecification(IBaseSpecification<T> rightSpecification, IBaseSpecification<T> leftSpecification)
        {
            this.rightSpecification = rightSpecification;
            this.leftSpecification = leftSpecification;
        }

        public bool IsSatisfiedBy(T entity)
        {
            return rightSpecification.IsSatisfiedBy(entity) && leftSpecification.IsSatisfiedBy(entity);
        }

        public Expression<Func<T, bool>> ToExpression()
        {
            var expr1 = rightSpecification.ToExpression();
            var expr2 = leftSpecification.ToExpression();

            var paramExpr = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(expr1.Body, expr2.Body);

            body = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(body);

            return Expression.Lambda<Func<T, bool>>(body, paramExpr);
        }


        public class ParameterReplacer : ExpressionVisitor
        {
            public readonly ParameterExpression _parameter;

            public ParameterReplacer(ParameterExpression parameter)
            {
                _parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return _parameter;
            }
        }
    }
}
