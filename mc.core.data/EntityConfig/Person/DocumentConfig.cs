using mc.core.domain.register.Entity.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.EntityConfig.Person
{
    internal class DocumentConfig : BaseConfig<Document>, IEntityTypeConfiguration<Document>
    {
        public override void Configure(EntityTypeBuilder<Document> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType($"{VARCHAR}(50)")
                .HasMaxLength(50);
            builder.Property(x => x.Value)
                .HasColumnType($"{VARCHAR}(50)")
                .HasMaxLength(50);
            builder.Property(x => x.EmissionDate)
                .HasColumnType(DATETIME);
            builder.Property(x => x.Complement)
                .HasColumnType($"{VARCHAR}(50)")
                .HasMaxLength(50);
        }
    }
}
