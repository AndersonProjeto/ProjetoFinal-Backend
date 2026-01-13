using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class ExercicioConfiguracoes : IEntityTypeConfiguration<Exercicio>
    {
        public void Configure(EntityTypeBuilder<Exercicio> builder)
        {
            builder.ToTable("Exercicios").HasKey(e => e.ExercicioId);
            builder.Property(e => e.ExercicioId).HasColumnName("ExercicioId").IsRequired();
            builder.Property(e => e.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(150);
            builder.Property(e => e.GrupoMuscular).HasColumnName("GrupoMuscular").IsRequired().HasMaxLength(80);
            builder.Property(e => e.Equipamento).HasColumnName("Equipamento").HasMaxLength(80);
        }
    }
}