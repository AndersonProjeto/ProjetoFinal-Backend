using ProjetoBackend.Aplicacao.DTOs.Usuario;
using ProjetoBackend.Dominio.DTOs.Usuario;
namespace ProjetoBackend.Aplicacao.Usuarios.Interfaces
{
    public interface IUsuarioAplicacao
    {
        Task<int> AdicionarUsuario(AdicionarUsuarioDTO adicionarUsuarioDTO);
        Task AtualizarUsuario(AtualizarUsuarioDTO atualizarUsuarioDTO);
        Task DeletarUsuario(int usuarioId);
        Task<UsuarioDetalhesDTO?> ObterId(int usuarioId);
        Task<UsuarioResumoDto?> ObterUsuarioResumo(int usuarioId);
        Task<UsuarioUltimaEvolucaoDto?> ObterUltimaEvolucao(int usuarioId);
        Task AlterarSenha(AlterarSenhaDTO dto);


    }
}