using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces.Queries.Specifications.Bases
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);

        Expression<Func<T, bool>> Predicate { get; }

        IQueryable<T> Prepare(IQueryable<T> query);

        IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query);

        ISpecification<T> And(Expression<Func<T, bool>> right);

        ISpecification<T> And(ISpecification<T> specification);

        ISpecification<T> InitEmpty();

        ISpecification<T> Not();

        ISpecification<T> Or(Expression<Func<T, bool>> right);

        ISpecification<T> Or(ISpecification<T> specification);

        T SatisfyingItemFrom(IQueryable<T> query);
    }
}