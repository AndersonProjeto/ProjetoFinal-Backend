using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoBackend.Dominio.Entidade;

namespace ProjetoBackend.Repositorio.Configuracoes
{
    public class TreinoExercicioConfiguracoes : IEntityTypeConfiguration<TreinoExercicio>
    {
        public void Configure(EntityTypeBuilder<TreinoExercicio> builder)
        {
            builder.ToTable("TreinoExercicios").HasKey(te => te.TreinoExercicioId);
            builder.Property(te => te.TreinoExercicioId).HasColumnName("TreinoExercicioId").IsRequired();
            builder.Property(te => te.TreinoId).HasColumnName("TreinoId").IsRequired();
            builder.Property(te => te.ExercicioId).HasColumnName("ExercicioId").IsRequired();
            builder.Property(te => te.Series).HasColumnName("Series").IsRequired();
            builder.Property(te => te.Repeticoes).HasColumnName("Repeticoes").IsRequired();
            builder.Property(te => te.DescansoSegundos).HasColumnName("DescansoSegundos").IsRequired();

            builder.HasOne(te => te.Treino)
                   .WithMany(t => t.TreinoExercicios)
                   .HasForeignKey(te => te.TreinoId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(te => te.Exercicio)
                   .WithMany(e => e.TreinoExercicios)
                   .HasForeignKey(te => te.ExercicioId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}