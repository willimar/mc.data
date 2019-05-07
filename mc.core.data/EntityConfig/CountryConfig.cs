using mc.core.domain.register.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.EntityConfig
{
    internal class CountryConfig : BaseConfig<Country>, IEntityTypeConfiguration<Country>
    {
        public override void Configure(EntityTypeBuilder<Country> builder)
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
        }
    }
}
