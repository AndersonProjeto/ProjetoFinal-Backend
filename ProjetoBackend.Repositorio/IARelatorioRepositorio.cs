using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class IARelatorioRepositorio : BaseRepositorio, IIARelatorioRepositorio
    {
        public IARelatorioRepositorio(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> AdicionarRelatorio(IARelatorio relatorio)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleAsync<int>(
                "spIARelatorioCriar",
                new
                {
                    relatorio.UsuarioId,
                    relatorio.Relatorio
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IARelatorio?> ObterUltimoRelatorio(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QuerySingleOrDefaultAsync<IARelatorio>(
                "spIARelatorioObterUltimo",
                new { UsuarioId = usuarioId },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<IARelatorio>> ListarRelatorios(int usuarioId)
        {
            using var conn = CriarConexao();

            return await conn.QueryAsync<IARelatorio>(
                "spIARelatorioListarPorUsuario",
                new { UsuarioId = usuarioId },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}