using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Repositorio.Contexto;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public abstract class BaseRepositorio
    {
        private readonly string _connectionString;

        protected BaseRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        protected IDbConnection CriarConexao()
        {
            return new SqlConnection(_connectionString);
        }
    }


}
