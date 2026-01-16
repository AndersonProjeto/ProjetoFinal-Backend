using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Repositorio.Contexto;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public abstract class BaseRepositorio
    {
        protected readonly IDbConnection _connection;

        protected BaseRepositorio(IConfiguration configuration)
        {
            _connection = new SqlConnection(
                configuration.GetConnectionString("DefaultConnection")
            );
        }
    }

}
