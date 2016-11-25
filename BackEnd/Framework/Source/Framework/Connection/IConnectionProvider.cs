using System;
using System.Data.Common;

namespace Framework.Connection
{
    public interface IConnectionProvider : IDisposable
    {
        string ConnectionString { get; }
        DbConnection GetConnection(bool requiresOpen = false);
    }
}
