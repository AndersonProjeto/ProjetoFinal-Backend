using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    /// <summary>
    /// No PostgreSQL os objetos sp* sao FUNCTIONS, chamadas com SQL de texto
    /// em vez de CommandType.StoredProcedure.
    /// </summary>
    public class IAInterecaoRepositorio : BaseRepositorio, IIAInteracaoRepositorio
    {
        public IAInterecaoRepositorio(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AdicionarIAInteracao(IAInteracao interacao)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                """
                SELECT "spIAInteracaoCriar"(@UsuarioId, @Pergunta, @Resposta)
                """,
                new
                {
                    interacao.UsuarioId,
                    interacao.Pergunta,
                    interacao.Resposta
                }
            );
        }

        public async Task<IEnumerable<IAInteracao>> ListarIAInteracoesPorUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<IAInteracao>(
                """
                SELECT * FROM "spIAInteracaoListarPorUsuario"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IAInteracao?> ObterUltimaInteracao(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<IAInteracao>(
                """
                SELECT * FROM "spIAInteracaoObterUltima"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<IAInteracao>> ListarUltimasInteracoes(int usuarioId, int quantidade)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<IAInteracao>(
                """
                SELECT * FROM "spIAInteracaoObterUltimos"(@UsuarioId, @Quantidade)
                """,
                new
                {
                    UsuarioId = usuarioId,
                    Quantidade = quantidade
                }
            );
        }
    }
}
