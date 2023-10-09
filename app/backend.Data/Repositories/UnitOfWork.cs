using backend.Core.Repositories;
using backend.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Repositories
{
    public class UnitOfWork : IDevUoW, IUnitOfWork
    {
        private readonly BackendDbContext _context;

        public ITransactionRepository TransactionRepo { get; }
        public IAccountRepository AccountRepo { get; }

        public UnitOfWork(BackendDbContext _context)
        {
            this._context = _context;

            TransactionRepo = new TransactionRepository(_context);
            AccountRepo = new AccountRepository(_context);
        }

        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.AddRange(entities);
        }

        public async Task<int> CompleteAsync(bool stopTracker = false)
        {
            try
            {
                var save = await _context.SaveChangesAsync();
                if (stopTracker) { _context.ChangeTracker.Clear(); }
                return save;
            }
            catch (Exception e)
            {
                return -99;
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public void EnsureDeleted()
        {
            _context.Database.EnsureDeleted();
        }
        public void Migrate()
        {
            _context.Database.Migrate();
        }
    }
}
