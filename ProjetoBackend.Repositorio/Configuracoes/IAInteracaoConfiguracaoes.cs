using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class IAInteracaoConfiguracoes : IEntityTypeConfiguration<IAInteracao>
    {
        public void Configure(EntityTypeBuilder<IAInteracao> builder)
        {
            builder.ToTable("IAInteracoes").HasKey(i => i.IAInteracaoId);
            builder.Property(i => i.IAInteracaoId).HasColumnName("IAInteracaoId").IsRequired();
            builder.Property(i => i.UsuarioId).HasColumnName("UsuarioId").IsRequired();
            builder.Property(i => i.Pergunta).HasColumnName("Pergunta").IsRequired().HasMaxLength(500);
            builder.Property(i => i.Resposta).HasColumnName("Resposta").IsRequired();
            builder.Property(i => i.DataHora).HasColumnName("DataHora").IsRequired();

            builder.HasOne(i => i.Usuario)
                   .WithMany(u => u.IAInteracoes)
                   .HasForeignKey(i => i.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}