using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class UsuarioConfiguracoes : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios").HasKey(u => u.UsuarioId);

            builder.Property(u => u.UsuarioId).HasColumnName("UsuarioId").IsRequired();
            builder.Property(u => u.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(150);
            builder.Property(u => u.Email).HasColumnName("Email").IsRequired().HasMaxLength(150);
            builder.Property(u => u.SenhaHash).HasColumnName("SenhaHash").IsRequired();
            builder.Property(u => u.DataNascimento).HasColumnName("DataNascimento").IsRequired();
            builder.Property(u => u.AlturaCm).HasColumnName("AlturaCm").HasColumnType("decimal(5,2)").IsRequired();
            builder.Property(u => u.DataCriacao).HasColumnName("DataCriacao").HasDefaultValueSql("SYSUTCDATETIME()").IsRequired(); 

            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
