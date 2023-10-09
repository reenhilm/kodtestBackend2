using backend.Core.Models;
using backend.Core.Repositories;
using backend.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(BackendDbContext context) : base(context) { }


        public override async Task<IEnumerable<Account>>? GetAllAsync()
        {
            return await dbContext.Accounts.ToListAsync();           
        }


        public override async Task<Account?> GetAsync(System.Guid id) =>
            await dbContext.Accounts.FirstOrDefaultAsync(c => c.Id == id);


        public override async Task<IEnumerable<Account?>> FindAsync(Expression<Func<Account, bool>> predicate) =>
            await dbContext.Accounts.Where(predicate).ToListAsync();


        public override void Add(Account entity)
        {
            dbContext.Accounts.Add(entity);
        }

        public override void AddRange(IEnumerable<Account> entities)
        {
            dbContext.Accounts.AddRange(entities);
        }

        public override void Update(Account entity)
        {
            dbContext.Accounts.Update(entity);
        }

        public override void Remove(Account entity)
        {
            dbContext.Accounts.Remove(entity);
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
