using mc.core.data.EntityConfig;
using mc.core.data.EntityConfig.Person;
using mc.core.domain.register.Entity;
using mc.core.domain.register.Entity.Person;
using mc.core.domain.register.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.Context
{
    public class DataContext: DbContext, IProvider
    {
        protected virtual DbSet<Country> Country { get; set; }
        protected virtual DbSet<State> State { get; set; }
        protected virtual DbSet<City> City { get; set; }
        protected virtual DbSet<Address> Address { get; set; }
        protected virtual DbSet<Document> Document { get; set; }
        protected virtual DbSet<PersonalContact> PersonalContact { get; set; }
        protected virtual DbSet<Person> Person { get; set; }

        public DataContext()
        { }

        public DataContext(DbContextOptions<DataContext> opcoes)
            :base(opcoes)
        { }

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
