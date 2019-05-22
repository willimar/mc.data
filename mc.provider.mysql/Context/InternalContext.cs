using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.ToTools;
using mc.provider.mysql.EntityConfig;
using mc.provider.mysql.EntityConfig.Person;
using Microsoft.EntityFrameworkCore;

namespace mc.provider.mysql.Context
{
    internal class InternalContext : DbContext
    {
        public InternalContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ForMySqlUseIdentityColumns();

            new CountryConfig().Configure(modelBuilder.Entity<Country>());
            new StateConfig().Configure(modelBuilder.Entity<State>());
            new CityConfig().Configure(modelBuilder.Entity<City>());

            new AddressConfig().Configure(modelBuilder.Entity<Address>());
            new DocumentConfig().Configure(modelBuilder.Entity<Document>());
            new PersonalContactConfig().Configure(modelBuilder.Entity<PersonalContact>());
            new PersonConfig().Configure(modelBuilder.Entity<Person>());
            new MySqlCepConfig().Configure(modelBuilder.Entity<MySqlCepImport>());

            modelBuilder.Entity<Country>().ToTable(nameof(Country));
        }
    }
}
