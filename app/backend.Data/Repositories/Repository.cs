using backend.Core.Repositories;
using backend.Data.Context;
using System.Linq.Expressions;

namespace backend.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly BackendDbContext Context;

        public Repository(BackendDbContext context)
        {
            Context = context;
        }


        public virtual void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TEntity?>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<TEntity>>? GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public virtual Task<TEntity?> GetAsync(System.Guid id)
        {
            throw new NotImplementedException();
        }

        public virtual void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
