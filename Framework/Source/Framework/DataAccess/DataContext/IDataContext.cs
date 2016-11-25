using System;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.DataAccess.DataContext
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        //void SyncObjectState<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        //void SyncObjectsStatePostCommit();
    }
}