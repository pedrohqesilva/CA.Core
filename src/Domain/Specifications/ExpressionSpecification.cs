using System;
using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class ExpressionSpecification<T> : SpecificationBuilder<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        public override Expression<Func<T, bool>> Predicate { get => _predicate; }

        public ExpressionSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate;
        }
    }
}