using Microsoft.EntityFrameworkCore;
using ProjetoBackend.Repositorio.Contexto;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public abstract class BaseRepositorio
    {
        protected readonly ProjetoContexto _contexto;
        protected readonly IDbConnection _connection;

        protected BaseRepositorio(ProjetoContexto contexto)
        {
            _contexto = contexto;
            _connection = contexto.Database.GetDbConnection();
        }
    }
}
