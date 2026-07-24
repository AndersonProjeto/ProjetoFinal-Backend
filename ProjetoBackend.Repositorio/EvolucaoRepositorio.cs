using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    /// <summary>
    /// No PostgreSQL os objetos sp* sao FUNCTIONS, nao procedures: procedure do
    /// Postgres nao devolve result set para o Dapper. Por isso todas as chamadas
    /// usam SQL de texto (SELECT ...) em vez de CommandType.StoredProcedure.
    /// Os identificadores vao aspeados porque o EF Core cria as tabelas em
    /// PascalCase e o Postgres rebaixaria os nomes sem as aspas.
    /// </summary>
    public class EvolucaoRepositorio : BaseRepositorio, IEvolucaoRepositorio
    {
        public EvolucaoRepositorio(IConfiguration configuration)
            : base(configuration)
        {
        }

        public async Task<int> AdicionarEvolucao(Evolucao evolucao)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                """
                SELECT "spEvolucaoCriar"(@UsuarioId, @PesoKg, @CinturaCm, @BracoCm, @CoxaCm, @DataRegistro)
                """,
                new
                {
                    evolucao.UsuarioId,
                    evolucao.PesoKg,
                    evolucao.CinturaCm,
                    evolucao.BracoCm,
                    evolucao.CoxaCm,
                    evolucao.DataRegistro
                }
            );
        }

        public async Task AtualizarEvolucao(Evolucao evolucao)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                """
                SELECT "spEvolucaoAtualizar"(@EvolucaoId, @PesoKg, @CinturaCm, @BracoCm, @CoxaCm)
                """,
                new
                {
                    evolucao.EvolucaoId,
                    evolucao.PesoKg,
                    evolucao.CinturaCm,
                    evolucao.BracoCm,
                    evolucao.CoxaCm
                }
            );
        }

        public async Task<Evolucao?> ObterPorId(int evolucaoId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Evolucao>(
                """
                SELECT * FROM "spEvolucaoObter"(@EvolucaoId)
                """,
                new { EvolucaoId = evolucaoId }
            );
        }

        public async Task<Evolucao?> ObterUltimaEvolucao(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Evolucao>(
                """
                SELECT * FROM "spEvolucaoObterUltima"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<Evolucao>> ListarPorUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<Evolucao>(
                """
                SELECT * FROM "spEvolucaoListarPorUsuario"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<EvolucaoResumoDTO?> ResumoEvolucao(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<EvolucaoResumoDTO>(
                """
                SELECT *
                FROM "vwEvolucaoResumo"
                WHERE "UsuarioId" = @UsuarioId
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<EvolucaoHistoricoDTO>(
                """
                SELECT *
                FROM "vwEvolucaoHistorico"
                WHERE "UsuarioId" = @UsuarioId
                ORDER BY "DataRegistro" DESC
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal> ObterPesoInicial(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal>(
                """
                SELECT "fnEvolucaoPesoInicial"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal> ObterDiferencaPeso(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal>(
                """
                SELECT "fnEvolucaoDiferencaPeso"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal?> ObterCinturaInicial(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal?>(
                """
                SELECT "fnEvolucaoCinturaInicial"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal?> ObterDiferencaCintura(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal?>(
                """
                SELECT "fnEvolucaoDiferencaCintura"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal?> ObterBracoInicial(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal?>(
                """
                SELECT "fnEvolucaoBracoInicial"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal?> ObterDiferencaBraco(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal?>(
                """
                SELECT "fnEvolucaoDiferencaBraco"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal?> ObterCoxaInicial(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal?>(
                """
                SELECT "fnEvolucaoCoxaInicial"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal?> ObterDiferencaCoxa(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal?>(
                """
                SELECT "fnEvolucaoDiferencaCoxa"(@UsuarioId)
                """,
                new { UsuarioId = usuarioId }
            );
        }
    }
}
