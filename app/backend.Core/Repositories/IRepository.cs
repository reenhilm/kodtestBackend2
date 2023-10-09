using System.Linq.Expressions;

namespace backend.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets single post from database asyncronously
        /// </summary>
        /// <param name="id">client</param>
        /// <exception cref="ArgumentNullException">Thrown when userId input arguments is null</exception>
        /// <returns></returns>
        Task<TEntity?> GetAsync(System.Guid id);


        /// <summary>
        /// Find by expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity?>> FindAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// Gets single post from database asyncronously
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when userId input arguments is null</exception>
        /// <returns></returns>
        Task<IEnumerable<TEntity>>? GetAllAsync();

        void Add(TEntity entity);
        //void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
