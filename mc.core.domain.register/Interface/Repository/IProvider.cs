using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.domain.register.Interface.Repository
{
    public interface IProvider
    {
        int Port { get; }
        string Ip { get; }
        string DataBaseName { get; }
        string Password { set; }
        string UserName { set; }
        void Dispose();
        T GetDataBse<T>();
    }
}
