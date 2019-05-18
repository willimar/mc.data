using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.provider.sqlserver.EntityConfig;
using mc.provider.sqlserver.EntityConfig.Person;
using Microsoft.EntityFrameworkCore;

namespace mc.provider.sqlserver.Context
{
    internal class InternalContext: DbContext
    {
        public InternalContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();

            new CountryConfig().Configure(modelBuilder.Entity<Country>());
            new StateConfig().Configure(modelBuilder.Entity<State>());
            new CityConfig().Configure(modelBuilder.Entity<City>());

            new AddressConfig().Configure(modelBuilder.Entity<Address>());
            new DocumentConfig().Configure(modelBuilder.Entity<Document>());
            new PersonalContactConfig().Configure(modelBuilder.Entity<PersonalContact>());
            new PersonConfig().Configure(modelBuilder.Entity<Person>());

            modelBuilder.Entity<Country>().ToTable(nameof(Country));
        }
    }
}
