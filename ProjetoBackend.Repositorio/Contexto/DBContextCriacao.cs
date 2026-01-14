using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProjetoBackend.Repositorio.Contexto
{
    public class DBContextCriacao : IDesignTimeDbContextFactory<ProjetoContexto>
    {
        public ProjetoContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjetoContexto>();

            optionsBuilder.UseSqlServer(
                "Server=NOTE229\\anderson\\SQLEXPRESS;Database=AcadIA;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new ProjetoContexto(optionsBuilder.Options);
        }
    }
}
