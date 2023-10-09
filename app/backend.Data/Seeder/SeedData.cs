using backend.Core.Models;
using backend.Core.Repositories;
using backend.Data.Context;
using backend.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeder
{
    public static class SeedData
    {
        public static async Task InitAsync(IDevUoW uow)
        {
            if (uow is null) throw new ArgumentNullException(nameof(uow));

            //creates Transactions
            var transactions = SeedDataHelper.GetTransactions().ToList();
            uow.AddRange<Transaction>(transactions);

            //creates Accounts
            var accountsForTransactions = SeedDataHelper.GetAccountsForTransactions(SeedDataArgs.Accounts, transactions);
            uow.AddRange(accountsForTransactions);

            await uow.CompleteAsync();
        }
    }
}
