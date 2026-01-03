using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class EvolucaoConfiguracoes : IEntityTypeConfiguration<Evolucao>
    {
        public void Configure(EntityTypeBuilder<Evolucao> builder)
        {
            builder.ToTable("Evolucoes").HasKey(e => e.EvolucaoId);

            builder.Property(e => e.EvolucaoId).HasColumnName("EvolucaoId").IsRequired();
            builder.Property(e => e.UsuarioId).HasColumnName("UsuarioId").IsRequired();
            builder.Property(e => e.PesoKg).HasColumnName("PesoKg").IsRequired().HasColumnType("decimal(5,2)");
            builder.Property(e => e.CinturaCm).HasColumnName("CinturaCm").HasColumnType("decimal(5,2)");
            builder.Property(e => e.BracoCm).HasColumnName("BracoCm").HasColumnType("decimal(5,2)");
            builder.Property(e => e.CoxaCm).HasColumnName("CoxaCm").HasColumnType("decimal(5,2)");
            builder.Property(e => e.DataRegistro).HasColumnName("DataRegistro").IsRequired();

            builder.HasOne(e => e.Usuario)
                   .WithMany(u => u.Evolucoes)
                   .HasForeignKey(e => e.UsuarioId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.UsuarioId);
            builder.HasIndex(e => e.DataRegistro);
        }
    }
}