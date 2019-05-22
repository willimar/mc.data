using mc.core.domain.register.Entity;
using mc.core.domain.register.ToTools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mc.provider.mysql.EntityConfig
{
    internal class MySqlCepConfig : IEntityTypeConfiguration<MySqlCepImport>
    {
        public void Configure(EntityTypeBuilder<MySqlCepImport> builder)
        {
            builder.HasKey("Cep");
            builder.Property(x => x.Cep)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Logradouro)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Endereco)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.EnderecoCompleto)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Cidade)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Bairro)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Estado)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Uf)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.Pais)
                .HasColumnType($"varchar(150)");
            builder.Property(x => x.PaisSigla)
                .HasColumnType($"varchar(150)");
        }
    }
}
