using Domain.Specifications.Interfaces;
using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class OrSpecification<T> : SpecificationBuilder<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public override Expression<Func<T, bool>> Predicate => _left.Predicate != null ? Or(_left.Predicate, _right.Predicate) : _right.Predicate;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        private static Expression<Func<T, bool>> Or(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            var visitor = new SwapVisitor(left.Parameters[0], right.Parameters[0]);
            var binaryExpression = Expression.OrElse(visitor.Visit(left.Body), right.Body);
            var lambda = Expression.Lambda<Func<T, bool>>(binaryExpression, right.Parameters);
            return lambda;
        }
    }
}