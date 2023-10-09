namespace backend.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITransactionRepository TransactionRepo { get; }
        IAccountRepository AccountRepo { get; }
        
        Task<int> CompleteAsync(bool stopTracker = false);
    }
}
