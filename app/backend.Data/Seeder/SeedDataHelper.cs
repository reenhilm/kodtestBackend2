using backend.Core.Models;
using backend.Shared.Helpers;
using System;
using System.Xml.Linq;

namespace backend.Data.Seeder
{
    public static class SeedDataHelper
    {
        public static IEnumerable<Account> GetAccountsForTransactions(string[] _accounts, IEnumerable<Transaction> transactions)
        {
            List<Account> allAccountListsForAllTransactions = new List<Account>();
            foreach (Transaction transaction in transactions)
            {              
                _accounts.Select(m => new Account
                {
                    Id = Guid.Parse(m),
                    Added = GetDateTime(),
                    Balance = transaction.Amount
                }
                ).ToList().ForEach(newAccount => allAccountListsForAllTransactions.Add(newAccount));
            }
            return allAccountListsForAllTransactions;
        }
        public static IEnumerable<Transaction> GetTransactions()
        {
            var dateTimeNow = GetDateTime();
            return SeedDataArgs.Transactions.Select(a => new Transaction
            {
                Id = Guid.NewGuid(),
                Added = dateTimeNow.AddDays(-1).AddHours(4),
                Amount = a,
            }).ToList();
        }

        public static IEnumerable<Account> GetAccounts()
        {
            var dateTimeNow = GetDateTime();
            return SeedDataArgs.Transactions.Select(a => new Account
            {
                Id = Guid.NewGuid(),
                Added = dateTimeNow.AddDays(-1).AddHours(4),
                Balance = 2
            }).ToList();
        }

        private static DateTime GetDateTime() => DateTime.Now;
    }
}
