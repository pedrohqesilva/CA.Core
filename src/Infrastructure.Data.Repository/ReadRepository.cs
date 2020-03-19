using Domain.Interfaces.Queries.Specifications.Bases;
using Domain.Interfaces.Repositories.Bases;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository.Bases
{
    public class ReadRepository<T> : IReadRepository<T> where T : class
    {
        private readonly BaseContext _context;

        public ReadRepository(BaseContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> AsQuerable()
        {
            return _context.Set<T>()
                .AsQueryable();
        }

        public virtual IQueryable<T> Where(ISpecification<T> specification)
        {
            var query = _context.Set<T>()
                .Where(specification.Predicate);

            return query;
        }

        public Task<bool> All(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<T>()
                .AllAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public Task<bool> Any(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<T>()
                .AnyAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<int> Count(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<T>()
                .CountAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<List<T>> Search(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = specification.Prepare(_context.Set<T>().AsQueryable())
                .ToListAsync(cancellationToken);

            return result;
        }

        public virtual Task<List<T>> Search(ISpecification<T> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = specification.Prepare(_context.Set<T>().AsQueryable())
                .Skip(pageNumber)
                .Take(pageSize);

            var entities = query.ToListAsync(cancellationToken);
            return entities;
        }

        public virtual Task<T> FirstOrDefault(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<T>()
                .FirstOrDefaultAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual ValueTask<T> Find(CancellationToken cancellationToken, params object[] keys)
        {
            var entity = _context.Set<T>()
                .FindAsync(keys, cancellationToken);

            return entity;
        }
    }
}