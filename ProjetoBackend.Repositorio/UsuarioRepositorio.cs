using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    using Dapper;
    using Microsoft.Extensions.Configuration;
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
                using var conn = CriarConexao();

                return await conn.QuerySingleAsync<int>(
                    "spUsuarioCriar",
                    new
                    {
                        usuario.Nome,
                        usuario.Email,
                        usuario.SenhaHash,
                        usuario.DataNascimento,
                        usuario.AlturaCm,
                        usuario.AvatarEstilo,
                        usuario.AvatarSeed
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            public async Task AtualizarUsuario(Usuario usuario)
            {
                using var conn = CriarConexao();

                await conn.ExecuteAsync(
                    "spUsuarioAtualizar",
                    new
                    {
                        usuario.UsuarioId,
                        usuario.Nome,
                        usuario.Email,
                        usuario.DataNascimento,
                        usuario.AlturaCm,
                        usuario.AvatarEstilo,
                        usuario.AvatarSeed
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

            public async Task DeletarUsuario(int usuarioId)
            {
                using var conn = CriarConexao();

                await conn.ExecuteAsync(
                    "spUsuarioDeletar",
                    new { UsuarioId = usuarioId },
                    commandType: CommandType.StoredProcedure
                );
            }

            public async Task<Usuario?> ObterPorID(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<Usuario>(
                    "spUsuarioObter",
                    new { UsuarioId = usuarioId },
                    commandType: CommandType.StoredProcedure
                );
            }

            public async Task<Usuario?> ObterPorEmail(string email)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<Usuario>(
                    "spUsuarioObterPorEmail",
                    new { Email = email },
                    commandType: CommandType.StoredProcedure
                );
            }

            public async Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<UsuarioResumoDto>(
                    "SELECT * FROM vwUsuarioResumo WHERE UsuarioId = @UsuarioId",
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task<UsuarioUltimaEvolucaoDto?> ObterUltimaEvolucao(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QueryFirstOrDefaultAsync<UsuarioUltimaEvolucaoDto>(
                    "SELECT * FROM vwUsuarioUltimaEvolucao WHERE UsuarioId = @UsuarioId",
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task<UsuarioDetalhesDTO?> ObterUsuarioDetalhes(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<UsuarioDetalhesDTO>(
                    "SELECT * FROM vwUsuarioDetalhes WHERE UsuarioId = @UsuarioId",
                    new { UsuarioId = usuarioId }
                );
            }
            public async Task AtualizarSenha(int usuarioId, string senhaHash)
            {
                using var conn = CriarConexao();

                await conn.ExecuteAsync(
                    "spUsuarioAtualizarSenha",
                    new
                    {
                        UsuarioId = usuarioId,
                        SenhaHash = senhaHash
                    },
                    commandType: CommandType.StoredProcedure
                );
            }

        }

    }
}