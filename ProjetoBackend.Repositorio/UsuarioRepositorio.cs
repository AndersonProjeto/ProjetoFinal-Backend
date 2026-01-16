using Dapper;
using Microsoft.Extensions.Configuration;
using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Dominio.DTOs.Usuario;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Contexto;
using ProjetoBackend.Repositorio.Interfaces;
using System.Data;

namespace ProjetoBackend.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<int> AdicionarUsuario(Usuario usuario)
        {
            return await _connection.QuerySingleAsync<int>(
                "spUsuarioCriar",
                new
                {
                    usuario.Nome,
                    usuario.Email,
                    usuario.SenhaHash,
                    usuario.DataNascimento,
                    usuario.AlturaCm
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await _connection.ExecuteAsync(
                "spUsuarioAtualizar",
                new
                {
                    usuario.UsuarioId,
                    usuario.Nome,
                    usuario.Email,
                    usuario.DataNascimento,
                    usuario.AlturaCm
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeletarUsuario(int usuarioId)
        {
             await _connection.ExecuteAsync(
                "spUsuarioDeletar",
                new
                {
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure
            );
        }
      
        public async Task<Usuario?> ObterPorID(int usuarioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<Usuario>(
                "spUsuarioObter",
                new
                {
                    UsuarioId = usuarioId
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return await _connection.QuerySingleOrDefaultAsync<Usuario>(
                "spUsuarioObterPorEmail",
                new
                {
                    Email = email
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<UsuarioResumoDto>(
                "SELECT * FROM vwUsuarioResumo WHERE UsuarioId = @UsuarioId",
                new { UsuarioId = usuarioId }
            );
        }
        public async Task<UsuarioUltimaEvolucaoDto?> ObterUltimaEvolucao(int usuarioId)
        {
            return await _connection.QueryFirstOrDefaultAsync<UsuarioUltimaEvolucaoDto>(
                "SELECT * FROM vwUsuarioUltimaEvolucao WHERE UsuarioId = @UsuarioId",
                new { UsuarioId = usuarioId }
            );
        }

        public async Task<UsuarioDetalhesDTO?> ObterUsuarioDetalhes(int usuarioId)
        {
            return await _connection.QuerySingleOrDefaultAsync<UsuarioDetalhesDTO>(
                "SELECT * FROM vwUsuarioDetalhes WHERE UsuarioId = @UsuarioId",
                new { UsuarioId = usuarioId }
            );
        }
    }
}