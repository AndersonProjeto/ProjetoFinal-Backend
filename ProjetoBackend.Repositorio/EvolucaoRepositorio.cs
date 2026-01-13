using Dapper;
using ProjetoBackend.Aplicacao.DTOs.Evolucao;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class EvolucaoRepositorio : BaseRepositorio, IEvolucaoRepositorio
    {
        public EvolucaoRepositorio(ProjetoContexto contexto) : base(contexto)
        {
        }
        public async Task<int> AdicionarEvolucao(Evolucao evolucao)
        {
            return await _connection.QuerySingleAsync<int>("spEvolucaoCriar", new { 
            evolucao.UsuarioId,
            evolucao.PesoKg,
            evolucao.CinturaCm,
            evolucao.BracoCm,
            evolucao.CoxaCm,
            },
            commandType: CommandType.StoredProcedure);
        }

        public async Task AtualizarEvolucao(Evolucao evolucao)
        {
            await _connection.ExecuteAsync("spEvolucaoAtualizar", new
            {
                evolucao.EvolucaoId,
                evolucao.PesoKg,
                evolucao.CinturaCm,
                evolucao.BracoCm,
                evolucao.CoxaCm,
            },
            commandType: CommandType.StoredProcedure);
        }


        public async Task<Evolucao?> ObterUltimaEvolucao(int usuarioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<Evolucao>(
                "spEvolucaoObterUltima", new
                {
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<EvolucaoResumoDTO?>> ResumoEvolucao(int usuarioId)
        {
            return await _connection.QueryAsync<EvolucaoResumoDTO>(
                @"SELECT *
                  FROM vwEvolucaoResumo
                  WHERE UsuarioId = @UsuarioId",
                new { UsuarioId = usuarioId }
            );
        }
        public async Task<IEnumerable<EvolucaoHistoricoDTO?>> HistoricoDeEvolucaoDoUsuario(int usuarioId)
        {
            return await _connection.QueryAsync<EvolucaoHistoricoDTO>(
                @"SELECT * FROM vwEvolucaoHistorico 
                WHERE UsuarioId = @UsuarioId
                ORDER BY DataRegistro DESC",
                new { UsuarioId = usuarioId }
                );
        }
        public async Task<decimal> ObterPesoInicial(int usuarioId)
        {
            return await _connection.ExecuteScalarAsync<decimal>(
                "SELECT dbo.fnEvolucaoPesoInicial(@UsuarioId)",
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<decimal> ObterDiferencaPeso(int usuarioId)
        {
            return await _connection.ExecuteScalarAsync<decimal>(
                "SELECT dbo.fnEvolucaoDiferencaPeso(@UsuarioId)",
                new { UsuarioId = usuarioId }
            );
        }
    }
}