using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Dominio.Entidade;
using ProjetoBackend.Repositorio.Interfaces;

namespace ProjetoBackend.Repositorio
{
    using Dapper;
    using Microsoft.Extensions.Configuration;

    // Namespace aninhado preservado do original: o registro no DI referencia
    // ProjetoBackend.Repositorio.ProjetoBackend.Repositorio (ver ServicosExtensoes).
    namespace ProjetoBackend.Repositorio
    {
        /// <summary>
        /// No PostgreSQL os objetos sp* sao FUNCTIONS, chamadas com SQL de texto
        /// em vez de CommandType.StoredProcedure. Identificadores vao aspeados
        /// porque o EF cria as tabelas em PascalCase.
        /// </summary>
        public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
        {
            public UsuarioRepositorio(IConfiguration configuration) : base(configuration)
            {
            }

            public async Task<int> AdicionarUsuario(Usuario usuario)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleAsync<int>(
                    """
                    SELECT "spUsuarioCriar"(@Nome, @Email, @SenhaHash, @DataNascimento, @AlturaCm, @AvatarEstilo, @AvatarSeed)
                    """,
                    new
                    {
                        usuario.Nome,
                        usuario.Email,
                        usuario.SenhaHash,
                        usuario.DataNascimento,
                        usuario.AlturaCm,
                        usuario.AvatarEstilo,
                        usuario.AvatarSeed
                    }
                );
            }

            public async Task AtualizarUsuario(Usuario usuario)
            {
                using var conn = CriarConexao();

                await conn.ExecuteAsync(
                    """
                    SELECT "spUsuarioAtualizar"(@UsuarioId, @Nome, @Email, @DataNascimento, @AlturaCm, @AvatarSeed, @AvatarEstilo)
                    """,
                    new
                    {
                        usuario.UsuarioId,
                        usuario.Nome,
                        usuario.Email,
                        usuario.DataNascimento,
                        usuario.AlturaCm,
                        usuario.AvatarSeed,
                        usuario.AvatarEstilo
                    }
                );
            }

            public async Task DeletarUsuario(int usuarioId)
            {
                using var conn = CriarConexao();

                await conn.ExecuteAsync(
                    """
                    SELECT "spUsuarioDeletar"(@UsuarioId)
                    """,
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task<Usuario?> ObterPorID(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<Usuario>(
                    """
                    SELECT * FROM "spUsuarioObter"(@UsuarioId)
                    """,
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task<Usuario?> ObterPorEmail(string email)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<Usuario>(
                    """
                    SELECT * FROM "spUsuarioObterPorEmail"(@Email)
                    """,
                    new { Email = email }
                );
            }

            public async Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<UsuarioResumoDto>(
                    """
                    SELECT * FROM "vwUsuarioResumo" WHERE "UsuarioId" = @UsuarioId
                    """,
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task<UsuarioUltimaEvolucaoDto?> ObterUltimaEvolucao(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QueryFirstOrDefaultAsync<UsuarioUltimaEvolucaoDto>(
                    """
                    SELECT * FROM "vwUsuarioUltimaEvolucao" WHERE "UsuarioId" = @UsuarioId
                    """,
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task<UsuarioDetalhesDTO?> ObterUsuarioDetalhes(int usuarioId)
            {
                using var conn = CriarConexao();

                return await conn.QuerySingleOrDefaultAsync<UsuarioDetalhesDTO>(
                    """
                    SELECT * FROM "vwUsuarioDetalhes" WHERE "UsuarioId" = @UsuarioId
                    """,
                    new { UsuarioId = usuarioId }
                );
            }

            public async Task AtualizarSenha(int usuarioId, string senhaHash)
            {
                using var conn = CriarConexao();

                await conn.ExecuteAsync(
                    """
                    SELECT "spUsuarioAtualizarSenha"(@UsuarioId, @SenhaHash)
                    """,
                    new
                    {
                        UsuarioId = usuarioId,
                        SenhaHash = senhaHash
                    }
                );
            }
        }
    }
}
