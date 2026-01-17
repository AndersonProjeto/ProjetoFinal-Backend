using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class IAInterecaoRepositorio : BaseRepositorio, IIAInteracaoRepositorio
    {
        public IAInterecaoRepositorio(IConfiguration configuration)
            : base(configuration)
        {
        }
        public async Task<int> AdicionarIAInteracao(IAInteracao interacao)
        {
            return await _connection.QuerySingleAsync<int>(
                "spIAInteracaoCriar",
                new
                {
                    interacao.UsuarioId,
                    interacao.Pergunta,
                    interacao.Resposta
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<IAInteracao>> ListarIAInteracoesPorUsuario(int usuarioId)
        {
            return await _connection.QueryAsync<IAInteracao>(
                "spIAInteracaoListarPorUsuario",
                new
                {
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure);

        }

        public async Task<IAInteracao?> ObterUltimaInteracao(int usuarioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<IAInteracao>(
                "spIAInteracaoObterUltima",
                new
                { 
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}