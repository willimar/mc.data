using mc.core.domain.register.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mc.provider.mysql.EntityConfig
{
    internal class CityConfig : BaseConfig<City>, IEntityTypeConfiguration<City>
    {
        public override void Configure(EntityTypeBuilder<City> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType($"{VARCHAR}(150)")
                .HasMaxLength(150);
            builder.Property(x => x.Initials)
                .HasColumnType($"{VARCHAR}(5)")
                .HasMaxLength(5);
            builder.Property(x => x.Code)
                .HasColumnType($"{VARCHAR}(5)")
                .HasMaxLength(5);
            builder
                .HasOne(y => y.State)
                .WithMany();
        }
    }
}
