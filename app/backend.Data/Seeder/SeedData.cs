using backend.Core.Models;
using backend.Core.Repositories;
using backend.Data.Context;
using backend.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace backend.Data.Seeder
{
    public static class SeedData
    {
        public static async Task InitAsync(IDevUoW uow)
        {
            if (uow is null) throw new ArgumentNullException(nameof(uow));

            Guid Account1 = Guid.Parse("74760610-ed76-40e9-b606-a8f96a71ba5b");
            uow.AccountRepo.Add(
                new Account()
                {
                    Id = Account1,
                    Balance = -1,
                    Transactions = new List<Transaction>
                    {
                        new Transaction() {
                            AccountId = Account1,
                            Added = DateTime.Now,
                            Amount = 2
                        },
                        new Transaction() {
                            AccountId = Account1,
                            Added = DateTime.Now,
                            Amount = -3
                        }

                    }
                });

            Guid Account2 = Guid.Parse("8a692943-8fc1-4d84-8684-1105b012f94e");
            uow.AccountRepo.Add(
                new Account()
                {
                    Id = Account2,
                    Balance = 0,
                    Transactions = new List<Transaction>()
                });

            Guid Account3 = Guid.Parse("df861211-6013-45e8-b28b-24825f87d9a2");
            uow.AccountRepo.Add(
                new Account()
                {
                    Id = Account3,
                    Balance = 1,
                    Transactions = new List<Transaction>
                    {
                        new Transaction() {
                            AccountId = Account1,
                            Added = DateTime.Now,
                            Amount = 1
                        }
                    }
                });

            await uow.CompleteAsync();
        }
    }
}
