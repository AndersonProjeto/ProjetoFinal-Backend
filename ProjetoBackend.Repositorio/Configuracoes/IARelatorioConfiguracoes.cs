using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class IARelatorioConfiguracoes : IEntityTypeConfiguration<IARelatorio>
    {
        public void Configure(EntityTypeBuilder<IARelatorio> builder)
        {
            builder.ToTable("IARelatorios").HasKey(r => r.IARelatorioId);

            builder.Property(r => r.IARelatorioId).HasColumnName("IARelatorioId").IsRequired();
            builder.Property(r => r.UsuarioId).HasColumnName("UsuarioId").IsRequired();
            builder.Property(r => r.Relatorio).HasColumnName("Relatorio").IsRequired();
            builder.Property(r => r.DataGerado).HasColumnName("DataGerado").IsRequired();

            builder.HasOne(r => r.Usuario)
                   .WithMany(u => u.IARelatorios)
                   .HasForeignKey(r => r.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
