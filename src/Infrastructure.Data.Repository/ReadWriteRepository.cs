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
    public class ReadWriteRepository<TEntity, TContext> : IReadWriteRepository<TEntity, TContext>
        where TEntity : class
        where TContext : BaseContext
    {
        private readonly TContext _contexto;

        public ReadWriteRepository(TContext context)
        {
            _contexto = context;
        }

        #region Read

        public virtual IQueryable<TEntity> AsQuerable()
        {
            return _contexto.Set<TEntity>()
                .AsQueryable();
        }

        public virtual IQueryable<TEntity> Where(ISpecification<TEntity> specification)
        {
            var query = _contexto.Set<TEntity>()
                .Where(specification.Predicate);

            return query;
        }

        public Task<bool> All(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<TEntity>()
                .AllAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public Task<bool> Any(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<TEntity>()
                .AnyAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<int> Count(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<TEntity>()
                .CountAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual Task<List<TEntity>> Search(ISpecification<TEntity> specification, CancellationToken cancellationToken)
        {
            var result = specification.Prepare(_contexto.Set<TEntity>().AsQueryable())
                .ToListAsync(cancellationToken);

            return result;
        }

        public virtual Task<List<TEntity>> Search(ISpecification<TEntity> specification, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = specification.Prepare(_contexto.Set<TEntity>().AsQueryable())
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
            var result = _contexto.Set<TEntity>()
                .FirstOrDefaultAsync(specification.Predicate, cancellationToken);

            return result;
        }

        public virtual ValueTask<TEntity> Find(CancellationToken cancellationToken, params object[] keys)
        {
            var entity = _contexto.Set<TEntity>()
                .FindAsync(keys, cancellationToken);

            return entity;
        }

        #endregion Read

        #region Write

        public virtual TEntity Add(TEntity entity)
        {
            var result = _contexto.Set<TEntity>().Add(entity);
            return result.Entity;
        }

        public virtual Task AddRange(IList<TEntity> entity, CancellationToken cancellationToken)
        {
            var result = _contexto.Set<TEntity>().AddRangeAsync(entity, cancellationToken);
            return result;
        }

        public void Attach(TEntity entity)
        {
            if (_contexto.Entry(entity).State == EntityState.Detached)
            {
                _contexto.Set<TEntity>().Attach(entity);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            _contexto.Set<TEntity>().Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
        }

        public Task<int> SaveChanges(CancellationToken cancellationToken)
        {
            return _contexto.SaveChangesAsync(cancellationToken);
        }

        #endregion Write
    }
}