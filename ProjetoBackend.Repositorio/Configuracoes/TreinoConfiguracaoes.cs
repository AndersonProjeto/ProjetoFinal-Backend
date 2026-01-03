using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class TreinoConfiguracoes : IEntityTypeConfiguration<Treino>
    {
        public void Configure(EntityTypeBuilder<Treino> builder)
        {
            builder.ToTable("Treinos").HasKey(t => t.TreinoId);
            builder.Property(t => t.TreinoId).HasColumnName("TreinoId").IsRequired();
            builder.Property(t => t.UsuarioId).HasColumnName("UsuarioId").IsRequired();
            builder.Property(t => t.NomeTreino).HasColumnName("NomeTreino").IsRequired().HasMaxLength(100);
            builder.Property(t => t.DataCriacao).HasColumnName("DataCriacao").IsRequired();

            builder.HasOne(t => t.Usuario)
                   .WithMany(u => u.Treinos)
                   .HasForeignKey(t => t.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
