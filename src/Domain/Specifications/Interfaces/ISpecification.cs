using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Specifications.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Predicate { get; }

        bool IsSatisfiedBy(T entity);

        T SatisfyingItemFrom(IQueryable<T> query);

        IQueryable<T> SatisfyingItemsFrom(IQueryable<T> query);

        IQueryable<T> Prepare(IQueryable<T> query);

        ISpecification<T> And(Expression<Func<T, bool>> right);

        ISpecification<T> And(ISpecification<T> specification);

        ISpecification<T> Or(Expression<Func<T, bool>> right);

        ISpecification<T> Or(ISpecification<T> specification);

        ISpecification<T> Not();

        ISpecification<T> InitEmpty();
    }
}