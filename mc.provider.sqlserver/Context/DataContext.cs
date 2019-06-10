using mc.core.repository;
using Microsoft.EntityFrameworkCore;

namespace mc.provider.sqlserver.Context
{
    public class DataContext : IProvider
    {
        private InternalContext _context;

        public int Port { get; set; }
        public string Ip { get; set; }
        public string DataBaseName { get; set; }
        public string Password { private get; set; }
        public string UserName { private get; set; }

        public DataContext()
        {
            this.Port = 0;
            this.Ip = @".\SQLEXPRESS";
            this.DataBaseName = "MCDATA";
            this.Password = "superwell";
            this.UserName = "sa";
        }

        public DataContext(int port, string ip, string dataBaseName, string password, string userName)
        {
            this.Port = port;
            this.Ip = ip;
            this.DataBaseName = dataBaseName;
            this.Password = password;
            this.UserName = userName;
            this.DataBasePrepare();
        }

        public core.repository.ISet<TEntity> GetSet<TEntity>() where TEntity : class, new()
        {
            return new DataSet<TEntity>(this._context);
        }

        public void DataBasePrepare()
        {
            const string CONNECTIONSTRING = @"Data Source={0}{4};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}";
            var builder = new DbContextOptionsBuilder();
            var connectionString = string.Format(CONNECTIONSTRING, this.Ip, this.DataBaseName, this.UserName, this.Password,
                    this.Port > 0 ? this.Port.ToString(",0") : string.Empty);
            builder.UseSqlServer(connectionString);
            this._context = new InternalContext(builder.Options);
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public void Dispose()
        {
            if (this._context != null)
            {
                this._context.Dispose();
                this._context = null;
            }             
        }

        public TContext GetContext<TContext>()
        {
            return (dynamic)this._context;
        }
    }
}
