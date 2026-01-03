using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoBackend.Repositorio.Contexto
{
    public class DBContextCriacao : IDesignTimeDbContextFactory<ProjetoContexto>
    {
        public ProjetoContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjetoContexto>();

            optionsBuilder.UseSqlServer(
                "Server=ANDERSON\\SQLEXPRESS;Database=AcadIA;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new ProjetoContexto(optionsBuilder.Options);
        }
    }
}
