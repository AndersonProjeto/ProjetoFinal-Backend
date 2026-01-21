using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
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
                "spEvolucaoCriar",
                new
                {
                    evolucao.UsuarioId,
                    evolucao.PesoKg,
                    evolucao.CinturaCm,
                    evolucao.BracoCm,
                    evolucao.CoxaCm
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task AtualizarEvolucao(Evolucao evolucao)
        {
            using var conn = CriarConexao();

            await conn.ExecuteAsync(
                "spEvolucaoAtualizar",
                new
                {
                    evolucao.EvolucaoId,
                    evolucao.PesoKg,
                    evolucao.CinturaCm,
                    evolucao.BracoCm,
                    evolucao.CoxaCm
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Evolucao?> ObterPorId(int evolucaoId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Evolucao>(
                "spEvolucaoObter",
                new { EvolucaoId = evolucaoId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<Evolucao?> ObterUltimaEvolucao(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<Evolucao>(
                "spEvolucaoObterUltima",
                new { UsuarioId = usuarioId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<EvolucaoResumoDTO?>> ResumoEvolucao(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<EvolucaoResumoDTO>(
                @"SELECT *
                  FROM vwEvolucaoResumo
                  WHERE UsuarioId = @UsuarioId",
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<EvolucaoHistoricoDTO>(
                @"SELECT *
                  FROM vwEvolucaoHistorico
                  WHERE UsuarioId = @UsuarioId
                  ORDER BY DataRegistro DESC",
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal> ObterPesoInicial(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal>(
                "SELECT dbo.fnEvolucaoPesoInicial(@UsuarioId)",
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal> ObterDiferencaPeso(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.ExecuteScalarAsync<decimal>(
                "SELECT dbo.fnEvolucaoDiferencaPeso(@UsuarioId)",
                new { UsuarioId = usuarioId }
            );
        }
    }
}