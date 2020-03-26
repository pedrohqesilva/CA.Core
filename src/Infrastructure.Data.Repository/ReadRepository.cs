using Domain.Services.Interfaces.Repositories;
using Domain.Specifications.Interfaces;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
    public class ReadRepository<TEntity, TContext> : IReadRepository<TEntity>
        where TEntity : class
        where TContext : BaseContext
    {
        private readonly TContext _context;

        public ReadRepository(TContext context)
        {
            _context = context;
        }

        public virtual IQueryable<TEntity> AsQuerable()
        {
            return _context.Set<TEntity>()
                .AsQueryable();
        }

        public virtual IQueryable<TEntity> Where(ISpecification<TEntity> specification)
        {
            var query = _context.Set<TEntity>()
                .Where(specification.Predicate);

            return query;
        }

        public Task<bool> All(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<TEntity>()
                .AllAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public Task<bool> Any(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<TEntity>()
                .AnyAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<int> Count(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<TEntity>()
                .CountAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<List<TEntity>> Search(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = specification.Prepare(_context.Set<TEntity>().AsQueryable())
                .ToListAsync(cancellationToken);

            return result;
        }

        public virtual Task<List<TEntity>> Search(ISpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = specification.Prepare(_context.Set<TEntity>().AsQueryable())
                .Skip(pageNumber)
                .Take(pageSize);

            var entities = query.ToListAsync(cancellationToken);
            return entities;
        }

        public virtual Task<List<TEntity>> ToList(CancellationToken cancellationToken)
        {
            var entities = AsQuerable()
                .ToListAsync(cancellationToken);

            return entities;
        }

        public virtual Task<TEntity> FirstOrDefault(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _context.Set<TEntity>()
                .FirstOrDefaultAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual ValueTask<TEntity> Find(CancellationToken cancellationToken, params object[] keys)
        {
            var entity = _context.Set<TEntity>()
                .FindAsync(keys, cancellationToken);

            return entity;
        }
    }
}