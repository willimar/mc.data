using mc.core.domain.register.Entity.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mc.provider.sqlserver.EntityConfig.Person
{
    internal class AddressConfig : BaseConfig<Address>, IEntityTypeConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.PublicPlace)
                .IsRequired()
                .HasColumnType($"{VARCHAR}(150)")
                .HasMaxLength(150);
            builder.Property(x => x.Number)
                .HasColumnType($"{VARCHAR}(7)")
                .HasMaxLength(7);
            builder.Property(x => x.Complement)
                .HasColumnType($"{VARCHAR}(50)")
                .HasMaxLength(50);
            builder.Property(x => x.PostalCode)
                .HasColumnType($"{VARCHAR}(10)")
                .HasMaxLength(10);
            builder.Property(x => x.District)
                .HasColumnType($"{VARCHAR}(50)")
                .HasMaxLength(50);
            builder
                .HasOne(y => y.City)
                .WithMany();
        }
    }
}
