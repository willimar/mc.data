using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mc.provider.mysql.EntityConfig.Person
{
    internal class PersonConfig : BaseConfig<mc.core.domain.register.Entity.Person.Person>, IEntityTypeConfiguration<mc.core.domain.register.Entity.Person.Person>
    {
        public override void Configure(EntityTypeBuilder<mc.core.domain.register.Entity.Person.Person> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType($"{VARCHAR}(150)")
                .HasMaxLength(150);
            builder.Property(x => x.BirthDate)
                .HasColumnType(DATETIME);

            builder
                .HasMany(x => x.PersonalContacts)
                .WithOne();
            builder
                .HasMany(x => x.Dependents)
                .WithOne();
            builder
                .HasMany(x => x.Addresses)
                .WithOne();
            builder
                .HasMany(x => x.Documents)
                .WithOne();
        }
    }
}
