using mc.core.domain.register.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.EntityConfig
{
    internal class StateConfig : BaseConfig<State>, IEntityTypeConfiguration<State>
    {
        public override void Configure(EntityTypeBuilder<State> builder)
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
                .HasOne(x => x.Country)
                .WithMany();
        }
    }
}
