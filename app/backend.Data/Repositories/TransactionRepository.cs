using backend.Core.Models;
using backend.Core.Repositories;
using backend.Data.Context;
//using backend.Shared.Extensions;
//using backend.Shared.Resources;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BackendDbContext context) : base(context) { }


        public override async Task<IEnumerable<Transaction>>? GetAllAsync()
        {
            return await dbContext.Transactions.ToListAsync();
        }


        public override async Task<Transaction?> GetAsync(System.Guid id)
        {
            return await dbContext.Transactions.FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<Transaction?>> FindAsync(Expression<Func<Transaction, bool>> predicate) =>
            await dbContext.Transactions.Where(predicate).ToListAsync();


        public override void Add(Transaction entity)
        {
            dbContext.Transactions.Add(entity);
        }

        public override void Update(Transaction entity)
        {
            dbContext.Transactions.Update(entity);
        }

        public override void Remove(Transaction entity)
        {
            dbContext.Transactions.Remove(entity);
        }


        /// <summary>
        /// Sets the generic context to its type
        /// </summary>
        public BackendDbContext dbContext
        {
            get { return Context; }
        }

    }
}
