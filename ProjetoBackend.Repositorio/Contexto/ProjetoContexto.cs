using Microsoft.EntityFrameworkCore;
using ProjetoBackend.Dominio;
using ProjetoBackend.Repositorio.Configuracoes;

namespace ProjetoBackend.Repositorio.Contexto
{
    public class ProjetoContexto : DbContext
    {
        public ProjetoContexto(DbContextOptions<ProjetoContexto> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Treino> Treinos { get; set; }
        public DbSet<Exercicio> Exercicios { get; set; }
        public DbSet<TreinoExercicio> TreinoExercicios { get; set; }
        public DbSet<Evolucao> Evolucoes { get; set; }
        public DbSet<IAInteracao> IAInteracoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracoes());
            modelBuilder.ApplyConfiguration(new TreinoConfiguracoes());
            modelBuilder.ApplyConfiguration(new ExercicioConfiguracoes());
            modelBuilder.ApplyConfiguration(new TreinoExercicioConfiguracoes());    
            modelBuilder.ApplyConfiguration(new EvolucaoConfiguracoes());
            modelBuilder.ApplyConfiguration(new IAInteracaoConfiguracoes());
        }
       
    }
}