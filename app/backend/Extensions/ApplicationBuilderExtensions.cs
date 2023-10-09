using System;
using System.Threading.Tasks;
using backend.Core.Repositories;
using backend.Data.Context;
using backend.Data.Seeder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace backend.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                IDevUoW uow = (IDevUoW)(serviceProvider.GetRequiredService<IUnitOfWork>());

                uow.EnsureDeleted();
                uow.Migrate();

#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    await SeedData.InitAsync(uow);
                }
                catch (Exception e)
                {
                    throw;
                }
#pragma warning restore CS0168 // Variable is declared but never used
            }

        }
    }
}
