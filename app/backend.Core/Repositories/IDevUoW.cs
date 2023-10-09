using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Core.Repositories
{
    public interface IDevUoW : IUnitOfWork
    {
        void EnsureDeleted();
        void Migrate();
        void AddRange<T>(IEnumerable<T> entities) where T : class;
    }
}
