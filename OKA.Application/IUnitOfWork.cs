using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKA.Application
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
