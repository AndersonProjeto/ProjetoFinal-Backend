using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    /// <summary>
    /// No PostgreSQL os objetos sp* sao FUNCTIONS, chamadas com SQL de texto
    /// em vez de CommandType.StoredProcedure. A tabela e "IARelatorio" no
    /// singular — ver comentario em Sql/Postgres/07_iarelatorio.sql.
    /// </summary>
    public class IARelatorioRepositorio : BaseRepositorio, IIARelatorioRepositorio
    {
        public IARelatorioRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AdicionarRelatorio(IARelatorio relatorio)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                """
                SELECT "spIARelatorioCriar"(@UsuarioId, @Relatorio)
                """,
                new
                {
                    relatorio.UsuarioId,
                    relatorio.Relatorio
                }
            );
        }

        public async Task<IARelatorio?> ObterUltimoRelatorio(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<IARelatorio>(
                """
                SELECT * FROM "spIARelatorioObterUltimo"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<IARelatorio>> ListarRelatorios(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<IARelatorio>(
                """
                SELECT * FROM "spIARelatorioListarPorUsuario"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }
    }
}
