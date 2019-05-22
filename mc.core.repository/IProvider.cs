using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.repository
{
    public interface IProvider: IDisposable
    {
        int Port { get; }
        string Ip { get; }
        string DataBaseName { get; }
        string Password { set; }
        string UserName { set; }

        ISet<TEntity> GetSet<TEntity>() where TEntity : class, new();
        void DataBasePrepare();
        TContext GetContext<TContext>();
        void SaveChanges();

    }
}
