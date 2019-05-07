using mc.core.domain.register.Entity.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.data.EntityConfig.Person
{
    internal class PersonalContactConfig : BaseConfig<PersonalContact>, IEntityTypeConfiguration<PersonalContact>
    {
        public override void Configure(EntityTypeBuilder<PersonalContact> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType($"{VARCHAR}(50)")
                .HasMaxLength(50);
        }
    }
}
